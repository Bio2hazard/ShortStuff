using System.Collections.Generic;

namespace ShortStuff.Domain.Entities
{
    interface IValidatable
    {
        IEnumerable<ValidationRule> GetBrokenRules();
    }
}
