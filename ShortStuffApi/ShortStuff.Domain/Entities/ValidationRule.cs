namespace ShortStuff.Domain.Entities
{
    public class ValidationRule
    {
        public string Name { get; set; }
        public string Rule { get; set; }

        public ValidationRule(string name, string rule)
        {
            Name = name;
            Rule = rule;
        }
    }
}