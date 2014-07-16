// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MaxDepthCloneInjector.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The max depth clone injector.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Repository.ValueInjecter
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    using FastMember;

    using Omu.ValueInjecter;

    /// <summary>
    /// The max depth clone injector.
    /// </summary>
    public class MaxDepthCloneInjector : SmartConventionInjection
    {
        /// <summary>
        /// The was learned.
        /// </summary>
        private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<KeyValuePair<Type, Type>, Path>> WasLearned =
            new ConcurrentDictionary<Type, ConcurrentDictionary<KeyValuePair<Type, Type>, Path>>();

        /// <summary>
        /// The _max depth.
        /// </summary>
        private int _maxDepth;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaxDepthCloneInjector"/> class.
        /// </summary>
        /// <param name="maxDepth">
        /// The max depth.
        /// </param>
        public MaxDepthCloneInjector(int maxDepth)
        {
            _maxDepth = maxDepth;
        }

        /// <summary>
        /// The name match.
        /// </summary>
        /// <param name="c">
        /// The c.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected bool NameMatch(SmartConventionInfo c)
        {
            return c.SourceProp.Name == c.TargetProp.Name;
        }

        /// <summary>
        /// The execute deep match.
        /// </summary>
        /// <param name="mi">
        /// The mi.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        protected void ExecuteDeepMatch(SmartMatchInfo mi)
        {
            var sourceVal = GetValue(mi.SourceProp, mi.Source);
            if (sourceVal == null)
            {
                return;
            }

            // for value types and string just return the value as is
            if (mi.SourceProp.PropertyType.IsValueType || mi.SourceProp.PropertyType == typeof(string))
            {
                SetValue(mi.TargetProp, mi.Target, sourceVal);
                return;
            }

            // handle arrays
            if (mi.SourceProp.PropertyType.IsArray)
            {
                var arr = sourceVal as Array;

                // ReSharper disable once PossibleNullReferenceException
                var arrayClone = arr.Clone() as Array;

                for (var index = 0; index < arr.Length; index++)
                {
                    var arriVal = arr.GetValue(index);
                    if (arriVal.GetType().IsValueType || arriVal is string)
                    {
                        continue;
                    }

                    if (_maxDepth > 1)
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        arrayClone.SetValue(Activator.CreateInstance(arriVal.GetType()).InjectFrom(new MaxDepthCloneInjector(_maxDepth - 1), arriVal), index);
                    }
                    else
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        arrayClone.SetValue(Activator.CreateInstance(arriVal.GetType()).InjectFrom<SmartConventionInjection>(arriVal), index);
                    }
                }

                SetValue(mi.TargetProp, mi.Target, arrayClone);
                return;
            }

            if (mi.SourceProp.PropertyType.IsGenericType)
            {
                // handle IEnumerable<> also ICollection<> IList<> List<>
                if (mi.SourceProp.PropertyType.GetGenericTypeDefinition().GetInterfaces().Contains(typeof(IEnumerable)))
                {
                    var genericArgument = mi.TargetProp.PropertyType.GetGenericArguments()[0];

                    var tlist = typeof(List<>).MakeGenericType(genericArgument);

                    var list = Activator.CreateInstance(tlist);

                    if (genericArgument.IsValueType || genericArgument == typeof(string))
                    {
                        var addRange = tlist.GetMethod("AddRange");
                        addRange.Invoke(list, new[] { sourceVal });
                    }
                    else
                    {
                        var addMethod = tlist.GetMethod("Add");

                        // ReSharper disable once PossibleNullReferenceException
                        foreach (var o in sourceVal as IEnumerable)
                        {
                            if (_maxDepth > 1)
                            {
                                addMethod.Invoke(list, new[] { Activator.CreateInstance(genericArgument).InjectFrom(new MaxDepthCloneInjector(_maxDepth - 1), o) });
                            }
                            else
                            {
                                addMethod.Invoke(list, new[] { Activator.CreateInstance(genericArgument).InjectFrom<SmartConventionInjection>(o) });
                            }
                        }
                    }

                    SetValue(mi.TargetProp, mi.Target, list);
                    return;
                }

                throw new NotImplementedException(string.Format("deep clonning for generic type {0} is not implemented", mi.SourceProp.Name));
            }

            // for simple object types create a new instace and apply the clone injection on it
            if (_maxDepth > 1)
            {
                SetValue(mi.TargetProp, mi.Target, Activator.CreateInstance(mi.TargetProp.PropertyType).InjectFrom(new MaxDepthCloneInjector(_maxDepth - 1), sourceVal));
            }
            else
            {
                SetValue(mi.TargetProp, mi.Target, Activator.CreateInstance(mi.TargetProp.PropertyType).InjectFrom<SmartConventionInjection>(sourceVal));
            }
        }

        /// <summary>
        /// The inject.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        protected override void Inject(object source, object target)
        {
            var sourceProps = source.GetProps();
            var targetProps = target.GetProps();

            var cacheEntry = WasLearned.GetOrAdd(GetType(), new ConcurrentDictionary<KeyValuePair<Type, Type>, Path>());

            var path = cacheEntry.GetOrAdd(new KeyValuePair<Type, Type>(source.GetType(), target.GetType()), pair => Learn(source, target));

            if (path == null)
            {
                return;
            }

            foreach (var pair in path.MatchingProps)
            {
                var sourceProp = sourceProps.GetByName(pair.Key);
                var targetProp = targetProps.GetByName(pair.Value);
                if (_maxDepth > 0)
                {
                    ExecuteDeepMatch(new SmartMatchInfo { Source = source, Target = target, SourceProp = sourceProp, TargetProp = targetProp });
                }
                else
                {
                    ExecuteMatch(new SmartMatchInfo { Source = source, Target = target, SourceProp = sourceProp, TargetProp = targetProp });
                }
            }
        }

        /// <summary>
        /// The set value.
        /// </summary>
        /// <param name="prop">
        /// The prop.
        /// </param>
        /// <param name="component">
        /// The component.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        protected override void SetValue(PropertyDescriptor prop, object component, object value)
        {
            var a = TypeAccessor.Create(component.GetType());
            a[component, prop.Name] = value;
        }

        /// <summary>
        /// The get value.
        /// </summary>
        /// <param name="prop">
        /// The prop.
        /// </param>
        /// <param name="component">
        /// The component.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        protected override object GetValue(PropertyDescriptor prop, object component)
        {
            var a = TypeAccessor.Create(component.GetType(), true);
            return a[component, prop.Name];
        }

        /// <summary>
        /// The learn.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <returns>
        /// The <see cref="Path"/>.
        /// </returns>
        private Path Learn(object source, object target)
        {
            Path path = null;
            var sourceProps = source.GetProps();
            var targetProps = target.GetProps();
            var smartConventionInfo = new SmartConventionInfo { SourceType = source.GetType(), TargetType = target.GetType() };

            for (var i = 0; i < sourceProps.Count; i++)
            {
                var sourceProp = sourceProps[i];
                smartConventionInfo.SourceProp = sourceProp;

                for (var j = 0; j < targetProps.Count; j++)
                {
                    var targetProp = targetProps[j];
                    smartConventionInfo.TargetProp = targetProp;

                    if (_maxDepth > 0)
                    {
                        if (!NameMatch(smartConventionInfo))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (!Match(smartConventionInfo))
                        {
                            continue;
                        }
                    }

                    if (path == null)
                    {
                        path = new Path { MatchingProps = new Dictionary<string, string> { { smartConventionInfo.SourceProp.Name, smartConventionInfo.TargetProp.Name } } };
                    }
                    else
                    {
                        path.MatchingProps.Add(smartConventionInfo.SourceProp.Name, smartConventionInfo.TargetProp.Name);
                    }
                }
            }

            return path;
        }

        /// <summary>
        /// The path.
        /// </summary>
        private class Path
        {
            /// <summary>
            /// Gets or sets the matching props.
            /// </summary>
            public IDictionary<string, string> MatchingProps { get; set; }
        }
    }
}