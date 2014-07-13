// ShortStuff.Domain
// ActionResult.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System;
using System.Collections.Generic;
using System.Linq;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Enums;

namespace ShortStuff.Domain.Helpers
{
    public class ActionResult<TDomain, TId> where TDomain : ValidatableBase
    {
        private TDomain _actionResult;
        private IEnumerable<TDomain> _actionResultSet;

        public ActionResult()
        {
            ActionStatus = new ActionStatus<TId>();
        }

        public ActionStatus<TId> ActionStatus { get; set; }
        public Exception ActionException { get; set; }
        public IList<ValidationRule> BrokenValidationRules { get; private set; }

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
            set { _actionResult = value; }
        }

        public IEnumerable<TDomain> ActionDataSet
        {
            get
            {
                if (_actionResultSet != null)
                {
                    return _actionResultSet;
                }
                return _actionResult != null ? new List<TDomain>
                {
                    _actionResult
                } : null;
            }
            set { _actionResultSet = value; }
        }

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

        public bool Validate()
        {
            if (ActionData == null)
            {
                SetRules(new List<ValidationRule>
                {
                    new ValidationRule("Data", "Data_Null")
                });
                ActionStatus.Status = ActionStatusEnum.ValidationError;
                return false;
            }
            return SetRules(ActionData.GetBrokenRules());
        }

        public bool ValidateUpdate()
        {
            if (ActionData == null)
            {
                SetRules(new List<ValidationRule>
                {
                    new ValidationRule("Data", "Data_Null")
                });
                ActionStatus.Status = ActionStatusEnum.ValidationError;
                return false;
            }
            return SetRules(ActionData.GetUpdateBrokenRules());
        }
    }
}
