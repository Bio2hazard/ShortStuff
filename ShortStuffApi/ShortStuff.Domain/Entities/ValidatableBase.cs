// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatableBase.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   Provides methods for inheriting classes to validate themselves.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Domain.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides methods for inheriting classes to validate themselves.
    /// </summary>
    public abstract class ValidatableBase : IValidatable
    {
        /// <summary>
        /// The list of broken validation rules.
        /// </summary>
        private List<ValidationRule> _brokenRules = new List<ValidationRule>();

        /// <summary>
        /// Clears the list of broken validation rules, validates itself for creation and returns the updated list of broken validation rules.
        /// </summary>
        /// <returns>
        /// A IEnumerable of broken validation rules.
        /// </returns>
        public IEnumerable<ValidationRule> GetBrokenRules()
        {
            _brokenRules.Clear();
            Validate();
            return _brokenRules;
        }

        /// <summary>
        /// Clears the list of broken validation rules, validates itself for update and returns the updated list of broken validation rules.
        /// </summary>
        /// <returns>
        /// A IEnumerable of broken validation rules.
        /// </returns>
        public IEnumerable<ValidationRule> GetUpdateBrokenRules()
        {
            _brokenRules.Clear();
            UpdateValidate();
            return _brokenRules;
        }

        /// <summary>
        /// Adds a broken rule to the list.
        /// </summary>
        /// <param name="brokenRule">
        /// The broken rule to be added.
        /// </param>
        protected void AddBrokenRule(ValidationRule brokenRule)
        {
            _brokenRules.Add(brokenRule);
        }

        /// <summary>
        /// Validates the class for creation.
        /// </summary>
        protected abstract void Validate();

        /// <summary>
        /// Validates the class for Update.
        /// </summary>
        protected abstract void UpdateValidate();
    }
}