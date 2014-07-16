// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActionResult.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The action result helper class is the universal return value of all services.
//   It can contain a single data element via ActionResult or a set of data elements via ActionResultSet.
//   It contains information through ActionStatus, a exception through ActionException and a list of validation errors 
//   through BrokenValidationRules.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Domain.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ShortStuff.Domain.Entities;
    using ShortStuff.Domain.Enums;

    /// <summary>
    /// The action result helper class is the universal return value of all services.
    /// It can contain a single data element via ActionResult or a set of data elements via ActionResultSet.
    /// It contains information through ActionStatus, a exception through ActionException and a list of validation errors
    /// through BrokenValidationRules.
    /// </summary>
    /// <typeparam name="TDomain">
    /// Validatable Type of the attached data.
    /// </typeparam>
    /// <typeparam name="TId">
    /// Type of the data's unique identifier.
    /// </typeparam>
    public class ActionResult<TDomain, TId>
        where TDomain : ValidatableBase
    {
        /// <summary>
        /// The single data result.
        /// </summary>
        private TDomain _actionResult;

        /// <summary>
        /// The data result set.
        /// </summary>
        private IEnumerable<TDomain> _actionResultSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResult{TDomain,TId}"/> class.
        /// </summary>
        public ActionResult()
        {
            ActionStatus = new ActionStatus<TId>();
        }

        /// <summary>
        /// Gets or sets the action status.
        /// </summary>
        public ActionStatus<TId> ActionStatus { get; set; }

        /// <summary>
        /// Gets or sets the action exception.
        /// </summary>
        public Exception ActionException { get; set; }

        /// <summary>
        /// Gets the broken validation rules.
        /// </summary>
        public IList<ValidationRule> BrokenValidationRules { get; private set; }

        /// <summary>
        /// Gets or sets the action data.
        /// If no singular action data exists, attempt to grab the first element of the set instead.
        /// </summary>
        public TDomain ActionData
        {
            get
            {
                if (_actionResult != null)
                {
                    return _actionResult;
                }

                return _actionResultSet != null ? _actionResultSet.FirstOrDefault() : null;
            }

            set
            {
                _actionResult = value;
            }
        }

        /// <summary>
        /// Gets or sets the action data set.
        /// If no action data set exists, attempt to grab the singlular action data instead.
        /// </summary>
        public IEnumerable<TDomain> ActionDataSet
        {
            get
            {
                if (_actionResultSet != null)
                {
                    return _actionResultSet;
                }

                return _actionResult != null ? new List<TDomain> { _actionResult } : null;
            }

            set
            {
                _actionResultSet = value;
            }
        }

        /// <summary>
        /// Validates the attached singular action data for creation.
        /// </summary>
        /// <returns>
        /// Boolean false if validation failed, true if validation succeeded.
        /// </returns>
        public bool Validate()
        {
            if (ActionData == null)
            {
                SetRules(new List<ValidationRule> { new ValidationRule("Data", "Data_Null") });
                ActionStatus.Status = ActionStatusEnum.ValidationError;
                return false;
            }

            return SetRules(ActionData.GetBrokenRules());
        }

        /// <summary>
        /// Validates the attached singular action data for update.
        /// </summary>
        /// <returns>
        /// Boolean false if validation failed, true if validation succeeded.
        /// </returns>
        public bool ValidateUpdate()
        {
            if (ActionData == null)
            {
                SetRules(new List<ValidationRule> { new ValidationRule("Data", "Data_Null") });
                ActionStatus.Status = ActionStatusEnum.ValidationError;
                return false;
            }

            return SetRules(ActionData.GetUpdateBrokenRules());
        }

        /// <summary>
        /// If any rules were broken, populate the BrokenValidationRules list, and set the ActionStatus to ValidationError.
        /// </summary>
        /// <param name="brokenRules">
        /// The broken rules.
        /// </param>
        /// <returns>
        /// Boolean false if any rules were broken, true if no rules were broken.
        /// </returns>
        private bool SetRules(IEnumerable<ValidationRule> brokenRules)
        {
            BrokenValidationRules = brokenRules as IList<ValidationRule> ?? brokenRules.ToList();
            if (BrokenValidationRules.Any())
            {
                ActionStatus.Status = ActionStatusEnum.ValidationError;
                return false;
            }

            return true;
        }
    }
}