// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeepCloneInjection.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The deep clone injection.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Repository.ValueInjecter
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Omu.ValueInjecter;

    /// <summary>
    /// The deep clone injection.
    /// </summary>
    public class DeepCloneInjection : SmartConventionInjection
    {
        /// <summary>
        /// The match.
        /// </summary>
        /// <param name="c">
        /// The c.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected override bool Match(SmartConventionInfo c)
        {
            return c.SourceProp.Name == c.TargetProp.Name;
        }

        /// <summary>
        /// The execute match.
        /// </summary>
        /// <param name="mi">
        /// The mi.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        protected override void ExecuteMatch(SmartMatchInfo mi)
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

                    // ReSharper disable once PossibleNullReferenceException
                    arrayClone.SetValue(Activator.CreateInstance(arriVal.GetType()).InjectFrom<DeepCloneInjection>(arriVal), index);
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
                            addMethod.Invoke(list, new[] { Activator.CreateInstance(genericArgument).InjectFrom<DeepCloneInjection>(o) });
                        }
                    }

                    SetValue(mi.TargetProp, mi.Target, list);
                    return;
                }

                throw new NotImplementedException(string.Format("deep clonning for generic type {0} is not implemented", mi.SourceProp.Name));
            }

            // for simple object types create a new instace and apply the clone injection on it
            SetValue(mi.TargetProp, mi.Target, Activator.CreateInstance(mi.TargetProp.PropertyType).InjectFrom<DeepCloneInjection>(sourceVal));
        }
    }
}