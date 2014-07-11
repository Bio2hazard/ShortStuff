// ShortStuff.Repository
// FastDeepCloneInjection.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System.ComponentModel;
using FastMember;

namespace ShortStuff.Repository.ValueInjecter
{
    public class FastDeepCloneInjection : DeepCloneInjection
    {
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
    }
}
