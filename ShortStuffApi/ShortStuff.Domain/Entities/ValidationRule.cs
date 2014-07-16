// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationRule.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The validation rule represents a human-readable explanation as to why validation failed.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Domain.Entities
{
    /// <summary>
    /// The validation rule represents a human-readable explanation as to why validation failed.
    /// </summary>
    public class ValidationRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRule"/> class.
        /// </summary>
        /// <param name="name">
        /// The name of the validation rule.
        /// </param>
        /// <param name="rule">
        /// The explanation of the rule that was broken.
        /// </param>
        public ValidationRule(string name, string rule)
        {
            Name = name;
            Rule = rule;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        private string Name { get; set; }

        /// <summary>
        /// Gets or sets the rule.
        /// </summary>
        private string Rule { get; set; }
    }
}