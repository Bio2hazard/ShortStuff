using System.Collections.Generic;

namespace ShortStuff.Domain.Entities
{
    public abstract class ValidatableBase : IValidatable
    {
        private List<ValidationRule> _brokenRules = new List<ValidationRule>();

        protected void AddBrokenRule(ValidationRule brokenRule)
        {
            _brokenRules.Add(brokenRule);
        }

        public IEnumerable<ValidationRule> GetBrokenRules()
        {
            _brokenRules.Clear();
            Validate();
            return _brokenRules;
        }

        protected abstract void Validate();
    }
}