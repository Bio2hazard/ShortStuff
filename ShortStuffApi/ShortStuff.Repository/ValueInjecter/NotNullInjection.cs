using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastMember;
using Omu.ValueInjecter;

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

