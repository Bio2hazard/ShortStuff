﻿// ShortStuff.Domain
// Topic.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System.Collections.Generic;

namespace ShortStuff.Domain.Entities
{
    public class Topic : EntityBase<int>
    {
        public string Name { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public IEnumerable<User> Subscribers { get; set; }

        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                AddBrokenRule(new ValidationRule("Name", "Name_Missing"));
            }
        }

        protected override void UpdateValidate()
        {
            if (Name != null && string.IsNullOrWhiteSpace(Name))
            {
                AddBrokenRule(new ValidationRule("Name", "Name_Whitespace"));
            }
        }
    }
}
