namespace ShortStuff.Repository.ValueInjecter
{
    public class NotNullInjection : SmartConventionInjection
    {
        protected override void ExecuteMatch(SmartMatchInfo mi)
        {
            var srcValue = GetValue(mi.SourceProp, mi.Source);
            if(srcValue != null) SetValue(mi.TargetProp, mi.Target, srcValue);
        }
    }
}

