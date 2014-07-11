// ShortStuff.Domain
// ValidatableBase.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System.Collections.Generic;

namespace ShortStuff.Domain.Entities
{
    public abstract class ValidatableBase : IValidatable
    {
        private List<ValidationRule> _brokenRules = new List<ValidationRule>();

        public IEnumerable<ValidationRule> GetBrokenRules()
        {
            _brokenRules.Clear();
            Validate();
            return _brokenRules;
        }

        protected void AddBrokenRule(ValidationRule brokenRule)
        {
            _brokenRules.Add(brokenRule);
        }

        public IEnumerable<ValidationRule> GetUpdateBrokenRules()
        {
            _brokenRules.Clear();
            UpdateValidate();
            return _brokenRules;
        }

        protected abstract void Validate();
        protected abstract void UpdateValidate();
    }
}
