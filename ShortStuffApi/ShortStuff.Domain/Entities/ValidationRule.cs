// ShortStuff.Domain
// ValidationRule.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

namespace ShortStuff.Domain.Entities
{
    public class ValidationRule
    {
        public ValidationRule(string name, string rule)
        {
            Name = name;
            Rule = rule;
        }

        public string Name { get; set; }
        public string Rule { get; set; }
    }
}
