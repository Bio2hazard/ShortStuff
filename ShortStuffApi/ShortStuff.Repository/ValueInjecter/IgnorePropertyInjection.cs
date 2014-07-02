using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortStuff.Repository.ValueInjecter
{
    public class IgnorePropertyInjection : FastOneCloneInjection
    {
        private readonly IList<string> _ignoredProperties;

        public IgnorePropertyInjection(IList<string> ignoredProperties = null)
        {
            _ignoredProperties = ignoredProperties;
        }

        protected override bool Match(SmartConventionInfo c)
        {
            if (_ignoredProperties != null && _ignoredProperties.Contains(c.SourceProp.Name))
                return false;
            else
            {
                return c.SourceProp.Name == c.TargetProp.Name;
            }
        }

    }
}
