using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastMember;
using Omu.ValueInjecter;

namespace ShortStuff.Repository.ValueInjecter
{
    public class CompositeInjection
    {
        private class Path
        {
            public IDictionary<string, string> MatchingProps { get; set; }
        }

        private ExpandoObject _compositeObject;
        private ICollection<KeyValuePair<string, object>> _compositeCollection;
     
        public CompositeInjection()
        {
            _compositeObject = new ExpandoObject();
            _compositeCollection = (ICollection<KeyValuePair<string, object>>)_compositeObject;

        }

        protected void SetValue(PropertyDescriptor prop, object component, object value)
        {
            var a = TypeAccessor.Create(component.GetType());
            a[component, prop.Name] = value;
        }

        protected object GetValue(PropertyDescriptor prop, object component)
        {
            var a = TypeAccessor.Create(component.GetType(), true);
            return a[component, prop.Name];
        }
        protected virtual bool Match(SmartConventionInfo c)
        {
            return c.SourceProp.Name == c.TargetProp.Name && c.SourceProp.PropertyType == c.TargetProp.PropertyType;
        }

        protected void ExecuteMatch(SmartMatchInfo mi)
        {
            var sourceValue = GetValue(mi.SourceProp, mi.Source);
            var targetValue = GetValue(mi.TargetProp, mi.Target);
            _compositeCollection.Add(new KeyValuePair<string, object>(mi.SourceProp.Name, sourceValue));
        }

        public object Inject(object source, object target)
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
                var sourceValue = GetValue(smartConventionInfo.SourceProp, source);
                if(sourceValue == null) continue;

                for (var j = 0; j < targetProps.Count; j++)
                {
                    var targetProp = targetProps[j];
                    smartConventionInfo.TargetProp = targetProp;
                    var targetValue = GetValue(smartConventionInfo.TargetProp, target);

                    if (sourceValue.Equals(targetValue) || !Match(smartConventionInfo)) continue;
                    if (path == null)
                        path = new Path
                        {
                            MatchingProps = new Dictionary<string, string> { { smartConventionInfo.SourceProp.Name, smartConventionInfo.TargetProp.Name } }
                        };
                    else path.MatchingProps.Add(smartConventionInfo.SourceProp.Name, smartConventionInfo.TargetProp.Name);
                }
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

            dynamic composite = _compositeObject;

            return composite;
        }
    }
}
