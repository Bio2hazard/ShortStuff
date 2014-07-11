// ShortStuff.Repository
// SmartConventionInjection.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using Omu.ValueInjecter;

namespace ShortStuff.Repository.ValueInjecter
{
    public class SmartConventionInjection : ValueInjection
    {
        private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<KeyValuePair<Type, Type>, Path>> WasLearned = new ConcurrentDictionary<Type, ConcurrentDictionary<KeyValuePair<Type, Type>, Path>>();

        protected virtual void SetValue(PropertyDescriptor prop, object component, object value)
        {
            prop.SetValue(component, value);
        }

        protected virtual object GetValue(PropertyDescriptor prop, object component)
        {
            return prop.GetValue(component);
        }

        /// <summary>
        ///     Determines if 2 properties match.
        ///     Match is determined by name and type.
        ///     The type check is lenient towards comparing base types to nullable types.
        /// </summary>
        protected virtual bool Match(SmartConventionInfo c)
        {
            return c.SourceProp.Name == c.TargetProp.Name && (c.SourceProp.PropertyType == c.TargetProp.PropertyType || Nullable.GetUnderlyingType(c.SourceProp.PropertyType) == c.TargetProp.PropertyType || c.SourceProp.PropertyType == Nullable.GetUnderlyingType(c.TargetProp.PropertyType));
        }

        protected virtual void ExecuteMatch(SmartMatchInfo mi)
        {
            SetValue(mi.TargetProp, mi.Target, GetValue(mi.SourceProp, mi.Source));
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
                ExecuteMatch(new SmartMatchInfo
                {
                    Source = source,
                    Target = target,
                    SourceProp = sourceProp,
                    TargetProp = targetProp
                });
            }
        }

        private class Path
        {
            public IDictionary<string, string> MatchingProps { get; set; }
        }
    }

    public class SmartConventionInfo
    {
        public Type SourceType { get; set; }
        public Type TargetType { get; set; }

        public PropertyDescriptor SourceProp { get; set; }
        public PropertyDescriptor TargetProp { get; set; }
    }

    public class SmartMatchInfo
    {
        public PropertyDescriptor SourceProp { get; set; }
        public PropertyDescriptor TargetProp { get; set; }
        public object Source { get; set; }
        public object Target { get; set; }
    }
}
