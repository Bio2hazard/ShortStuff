// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpandoInjection.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The expando injection.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Repository.ValueInjecter
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Dynamic;

    using FastMember;

    using Omu.ValueInjecter;

    /// <summary>
    /// The expando injection.
    /// </summary>
    public class ExpandoInjection
    {
        /// <summary>
        /// The _composite collection.
        /// </summary>
        private ICollection<KeyValuePair<string, object>> _compositeCollection;

        /// <summary>
        /// The _composite object.
        /// </summary>
        private ExpandoObject _compositeObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpandoInjection"/> class.
        /// </summary>
        public ExpandoInjection()
        {
            _compositeObject = new ExpandoObject();
            _compositeCollection = _compositeObject;
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
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object Inject(object source, object target)
        {
            Path path = null;
            var sourceProps = source.GetProps();
            var targetProps = target.GetProps();
            var smartConventionInfo = new SmartConventionInfo { SourceType = source.GetType(), TargetType = target.GetType() };

            for (var i = 0; i < sourceProps.Count; i++)
            {
                var sourceProp = sourceProps[i];
                smartConventionInfo.SourceProp = sourceProp;
                var sourceValue = GetValue(smartConventionInfo.SourceProp, source);
                if (sourceValue == null)
                {
                    continue;
                }

                for (var j = 0; j < targetProps.Count; j++)
                {
                    var targetProp = targetProps[j];
                    smartConventionInfo.TargetProp = targetProp;
                    var targetValue = GetValue(smartConventionInfo.TargetProp, target);

                    if (sourceValue.Equals(targetValue) || !Match(smartConventionInfo))
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

            if (path != null)
            {
                foreach (var pair in path.MatchingProps)
                {
                    var sourceProp = sourceProps.GetByName(pair.Key);
                    var targetProp = targetProps.GetByName(pair.Value);
                    ExecuteMatch(new SmartMatchInfo { Source = source, Target = target, SourceProp = sourceProp, TargetProp = targetProp });
                }
            }

            dynamic composite = _compositeObject;

            return composite;
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
        protected void SetValue(PropertyDescriptor prop, object component, object value)
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
        protected object GetValue(PropertyDescriptor prop, object component)
        {
            var a = TypeAccessor.Create(component.GetType(), true);
            return a[component, prop.Name];
        }

        /// <summary>
        /// The match.
        /// </summary>
        /// <param name="c">
        /// The c.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected virtual bool Match(SmartConventionInfo c)
        {
            return c.SourceProp.Name == c.TargetProp.Name && c.SourceProp.PropertyType == c.TargetProp.PropertyType;
        }

        /// <summary>
        /// The execute match.
        /// </summary>
        /// <param name="mi">
        /// The mi.
        /// </param>
        protected void ExecuteMatch(SmartMatchInfo mi)
        {
            var sourceValue = GetValue(mi.SourceProp, mi.Source);
            _compositeCollection.Add(new KeyValuePair<string, object>(mi.SourceProp.Name, sourceValue));
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