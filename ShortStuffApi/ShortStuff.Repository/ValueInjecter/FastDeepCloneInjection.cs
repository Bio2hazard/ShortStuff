// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FastDeepCloneInjection.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The fast deep clone injection.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Repository.ValueInjecter
{
    using System.ComponentModel;

    using FastMember;

    /// <summary>
    /// The fast deep clone injection.
    /// </summary>
    public class FastDeepCloneInjection : DeepCloneInjection
    {
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
    }
}