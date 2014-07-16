// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotNullInjection.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The not null injection.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Repository.ValueInjecter
{
    /// <summary>
    /// The not null injection.
    /// </summary>
    public class NotNullInjection : SmartConventionInjection
    {
        /// <summary>
        /// The execute match.
        /// </summary>
        /// <param name="mi">
        /// The mi.
        /// </param>
        protected override void ExecuteMatch(SmartMatchInfo mi)
        {
            var srcValue = GetValue(mi.SourceProp, mi.Source);
            if (srcValue != null)
            {
                SetValue(mi.TargetProp, mi.Target, srcValue);
            }
        }
    }
}