// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Topic.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The topic business model which is equivalent to the topic data entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Domain.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// The topic business model which is equivalent to the topic data entity.
    /// </summary>
    public class Topic : EntityBase<int>
    {
        /// <summary>
        /// Gets or sets the name of the topic.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of messages that are a part of this topic.
        /// </summary>
        public IEnumerable<Message> Messages { get; set; }

        /// <summary>
        /// Gets or sets the list of users who are subscribed to this topic.
        /// </summary>
        public IEnumerable<User> Subscribers { get; set; }

        /// <summary>
        /// Validates the topic for creation.
        /// </summary>
        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                AddBrokenRule(new ValidationRule("Name", "Name_Missing"));
            }
        }

        /// <summary>
        /// Validates the topic for update.
        /// </summary>
        protected override void UpdateValidate()
        {
            if (Name != null && string.IsNullOrWhiteSpace(Name))
            {
                AddBrokenRule(new ValidationRule("Name", "Name_Whitespace"));
            }
        }
    }
}