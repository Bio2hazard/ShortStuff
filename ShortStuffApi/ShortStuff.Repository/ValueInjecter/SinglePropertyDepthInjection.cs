// ShortStuff.Repository
// SinglePropertyDepthInjection.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FastMember;
using Omu.ValueInjecter;

namespace ShortStuff.Repository.ValueInjecter
{
    public class SinglePropertyDepthInjection : SmartConventionInjection
    {
        private Dictionary<string, int> _propertyDict;

        public SinglePropertyDepthInjection(Dictionary<string, int> propertyDict)
        {
            _propertyDict = propertyDict;
        }

        protected bool NameMatch(SmartConventionInfo c)
        {
            return c.SourceProp.Name == c.TargetProp.Name;
        }

        protected void ExecuteDeepMatch(SmartMatchInfo mi)
        {
            var sourceVal = GetValue(mi.SourceProp, mi.Source);
            if (sourceVal == null)
            {
                return;
            }

            //for value types and string just return the value as is
            if (mi.SourceProp.PropertyType.IsValueType || mi.SourceProp.PropertyType == typeof (string))
            {
                SetValue(mi.TargetProp, mi.Target, sourceVal);
                return;
            }

            int propDepth;
            var propFound = _propertyDict.TryGetValue(mi.SourceProp.Name, out propDepth);

            //handle arrays
            if (mi.SourceProp.PropertyType.IsArray)
            {
                var arr = sourceVal as Array;
// ReSharper disable once PossibleNullReferenceException
                var arrayClone = arr.Clone() as Array;

                for (var index = 0; index < arr.Length; index++)
                {
                    var arriVal = arr.GetValue(index);
                    if (arriVal.GetType()
                               .IsValueType || arriVal is string)
                    {
                        continue;
                    }
                    if (propFound && propDepth > 0)
                    {
// ReSharper disable once PossibleNullReferenceException
                        arrayClone.SetValue(Activator.CreateInstance(arriVal.GetType())
                                                     .InjectFrom(new MaxDepthCloneInjector(propDepth), arriVal), index);
                    }
                    else
                    {
// ReSharper disable once PossibleNullReferenceException
                        arrayClone.SetValue(Activator.CreateInstance(arriVal.GetType())
                                                     .InjectFrom<SmartConventionInjection>(arriVal), index);
                    }
                }
                SetValue(mi.TargetProp, mi.Target, arrayClone);
                return;
            }

            if (mi.SourceProp.PropertyType.IsGenericType)
            {
                //handle IEnumerable<> also ICollection<> IList<> List<>
                if (mi.SourceProp.PropertyType.GetGenericTypeDefinition()
                      .GetInterfaces()
                      .Contains(typeof (IEnumerable)))
                {
                    var genericArgument = mi.TargetProp.PropertyType.GetGenericArguments()[0];

                    var tlist = typeof (List<>).MakeGenericType(genericArgument);

                    var list = Activator.CreateInstance(tlist);

                    if (genericArgument.IsValueType || genericArgument == typeof (string))
                    {
                        var addRange = tlist.GetMethod("AddRange");
                        addRange.Invoke(list, new[]
                        {
                            sourceVal
                        });
                    }
                    else
                    {
                        var addMethod = tlist.GetMethod("Add");
// ReSharper disable once PossibleNullReferenceException
                        foreach (var o in sourceVal as IEnumerable)
                        {
                            if (propFound && propDepth > 0)
                            {
                                addMethod.Invoke(list, new[]
                                {
                                    Activator.CreateInstance(genericArgument)
                                             .InjectFrom(new MaxDepthCloneInjector(propDepth), o)
                                });
                            }
                            else
                            {
                                addMethod.Invoke(list, new[]
                                {
                                    Activator.CreateInstance(genericArgument)
                                             .InjectFrom<SmartConventionInjection>(o)
                                });
                            }
                        }
                    }
                    SetValue(mi.TargetProp, mi.Target, list);
                    return;
                }

                throw new NotImplementedException(string.Format("deep clonning for generic type {0} is not implemented", mi.SourceProp.Name));
            }

            //for simple object types create a new instace and apply the clone injection on it
            if (propFound && propDepth > 0)
            {
                SetValue(mi.TargetProp, mi.Target, Activator.CreateInstance(mi.TargetProp.PropertyType)
                                                            .InjectFrom(new MaxDepthCloneInjector(propDepth), sourceVal));
            }
            else
            {
                SetValue(mi.TargetProp, mi.Target, Activator.CreateInstance(mi.TargetProp.PropertyType)
                                                            .InjectFrom<SmartConventionInjection>(sourceVal));
            }
        }

        private Path Learn(object source, object target)
        {
            Path path = null;
            var sourceProps = source.GetProps();
            var targetProps = target.GetProps();
            var smartConventionInfo = new SmartConventionInfo
            {
                SourceType = source.GetType(),
                TargetType = target.GetType()
            };

            for (var i = 0; i < sourceProps.Count; i++)
            {
                var sourceProp = sourceProps[i];
                smartConventionInfo.SourceProp = sourceProp;

                var propFound = _propertyDict.ContainsKey(sourceProp.Name);

                // If the source prop's name is not in the allowed list, and is neither a value type nor a string, we need to check whether we should recurse or not
                if (!propFound && !sourceProp.PropertyType.IsValueType && sourceProp.PropertyType != typeof (string))
                {
                    // If source property is a Class ( and thus a navigational property ) and it was not found in the dictionary: Skip it
                    if (sourceProp.PropertyType.IsClass)
                    {
                        continue;
                    }
                    if (sourceProp.PropertyType.IsGenericType && sourceProp.PropertyType.GetGenericArguments()[0].IsClass)
                    {
                        continue;
                    }
                }

                for (var j = 0; j < targetProps.Count; j++)
                {
                    var targetProp = targetProps[j];
                    smartConventionInfo.TargetProp = targetProp;

                    if (!NameMatch(smartConventionInfo))
                    {
                        continue;
                    }

                    if (path == null)
                    {
                        path = new Path
                        {
                            MatchingProps = new Dictionary<string, string>
                            {
                                {
                                    smartConventionInfo.SourceProp.Name, smartConventionInfo.TargetProp.Name
                                }
                            }
                        };
                    }
                    else
                    {
                        path.MatchingProps.Add(smartConventionInfo.SourceProp.Name, smartConventionInfo.TargetProp.Name);
                    }
                }
            }
            return path;
        }

        protected override void Inject(object source, object target)
        {
            var sourceProps = source.GetProps();
            var targetProps = target.GetProps();

            var path = Learn(source, target);

            if (path == null)
            {
                return;
            }

            foreach (var pair in path.MatchingProps)
            {
                var sourceProp = sourceProps.GetByName(pair.Key);
                var targetProp = targetProps.GetByName(pair.Value);
                int propDepth;
                var propFound = _propertyDict.TryGetValue(pair.Key, out propDepth);

                if (propFound)
                {
                    ExecuteDeepMatch(new SmartMatchInfo
                    {
                        Source = source,
                        Target = target,
                        SourceProp = sourceProp,
                        TargetProp = targetProp
                    });
                }
                else
                {
                    ExecuteMatch(new SmartMatchInfo
                    {
                        Source = source,
                        Target = target,
                        SourceProp = sourceProp,
                        TargetProp = targetProp
                    });
                }
            }
        }

        protected override void SetValue(PropertyDescriptor prop, object component, object value)
        {
            var a = TypeAccessor.Create(component.GetType());
            a[component, prop.Name] = value;
        }

        protected override object GetValue(PropertyDescriptor prop, object component)
        {
            var a = TypeAccessor.Create(component.GetType(), true);
            return a[component, prop.Name];
        }

        private class Path
        {
            public IDictionary<string, string> MatchingProps { get; set; }
        }

        public struct PropPair
        {
            public int Depth;
            public bool Ignored;
        }
    }
}
