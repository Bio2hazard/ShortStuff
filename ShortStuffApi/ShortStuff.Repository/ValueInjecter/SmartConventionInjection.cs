// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SmartConventionInjection.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The smart convention injection.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Repository.ValueInjecter
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.ComponentModel;

    using Omu.ValueInjecter;

    /// <summary>
    /// The smart convention injection.
    /// </summary>
    public class SmartConventionInjection : ValueInjection
    {
        /// <summary>
        /// The was learned.
        /// </summary>
        private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<KeyValuePair<Type, Type>, Path>> WasLearned =
            new ConcurrentDictionary<Type, ConcurrentDictionary<KeyValuePair<Type, Type>, Path>>();

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
        protected virtual void SetValue(PropertyDescriptor prop, object component, object value)
        {
            prop.SetValue(component, value);
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
        protected virtual object GetValue(PropertyDescriptor prop, object component)
        {
            return prop.GetValue(component);
        }

        /// <summary>
        /// Determines if 2 properties match.
        ///     Match is determined by name and type.
        ///     The type check is lenient towards comparing base types to nullable types.
        /// </summary>
        /// <param name="c">
        /// The c.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected virtual bool Match(SmartConventionInfo c)
        {
            return c.SourceProp.Name == c.TargetProp.Name
                   && (c.SourceProp.PropertyType == c.TargetProp.PropertyType || Nullable.GetUnderlyingType(c.SourceProp.PropertyType) == c.TargetProp.PropertyType
                       || c.SourceProp.PropertyType == Nullable.GetUnderlyingType(c.TargetProp.PropertyType));
        }

        /// <summary>
        /// The execute match.
        /// </summary>
        /// <param name="mi">
        /// The mi.
        /// </param>
        protected virtual void ExecuteMatch(SmartMatchInfo mi)
        {
            SetValue(mi.TargetProp, mi.Target, GetValue(mi.SourceProp, mi.Source));
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
                ExecuteMatch(new SmartMatchInfo { Source = source, Target = target, SourceProp = sourceProp, TargetProp = targetProp });
            }
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

                    if (!Match(smartConventionInfo))
                    {
                        continue;
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

    /// <summary>
    /// The smart convention info.
    /// </summary>
    public class SmartConventionInfo
    {
        /// <summary>
        /// Gets or sets the source type.
        /// </summary>
        public Type SourceType { get; set; }

        /// <summary>
        /// Gets or sets the target type.
        /// </summary>
        public Type TargetType { get; set; }

        /// <summary>
        /// Gets or sets the source prop.
        /// </summary>
        public PropertyDescriptor SourceProp { get; set; }

        /// <summary>
        /// Gets or sets the target prop.
        /// </summary>
        public PropertyDescriptor TargetProp { get; set; }
    }

    /// <summary>
    /// The smart match info.
    /// </summary>
    public class SmartMatchInfo
    {
        /// <summary>
        /// Gets or sets the source prop.
        /// </summary>
        public PropertyDescriptor SourceProp { get; set; }

        /// <summary>
        /// Gets or sets the target prop.
        /// </summary>
        public PropertyDescriptor TargetProp { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        public object Source { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        public object Target { get; set; }
    }
}