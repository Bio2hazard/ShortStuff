// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValidatable.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The Validatable interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Domain.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// The Validatable interface.
    /// </summary>
    internal interface IValidatable
    {
        /// <summary>
        /// Every inheriting member must be able to validate itself for creation through GetBrokenRules, 
        /// and return a IEnumerable of broken rules should the validation fail.
        /// </summary>
        /// <returns>
        /// A IEnumerable of broken validation rules.
        /// </returns>
        IEnumerable<ValidationRule> GetBrokenRules();

        /// <summary>
        /// Every inheriting member must be able to validate itself for update through GetUpdateBrokenRules, 
        /// and return a IEnumerable of broken rules should the validation fail.
        /// </summary>
        /// <returns>
        /// A IEnumerable of broken validation rules.
        /// </returns>
        IEnumerable<ValidationRule> GetUpdateBrokenRules();
    }
}