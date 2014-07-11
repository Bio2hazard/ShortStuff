// ShortStuff.Repository
// NotNullInjection.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

namespace ShortStuff.Repository.ValueInjecter
{
    public class NotNullInjection : SmartConventionInjection
    {
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
