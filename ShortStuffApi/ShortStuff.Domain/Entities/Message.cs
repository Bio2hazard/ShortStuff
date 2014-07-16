// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Message.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The message business model which is equivalent to the message data entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The message business model which is equivalent to the message data entity.
    /// </summary>
    public class Message : EntityBase<int>
    {
        /// <summary>
        /// Gets or sets the creator of the message.
        /// </summary>
        public User Creator { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the creator of the message.
        /// </summary>
        public decimal? CreatorId { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the message.
        /// </summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the content of the message.
        /// </summary>
        public string MessageContent { get; set; }

        /// <summary>
        /// Gets or sets the parent of the message.
        /// </summary>
        public Message ParentMessage { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the parent of the message.
        /// </summary>
        public int? ParentMessageId { get; set; }

        /// <summary>
        /// Gets or sets the list of messages posted as a reply to this message.
        /// </summary>
        public IEnumerable<Message> Replies { get; set; }

        /// <summary>
        /// Validates the message for creation.
        /// </summary>
        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(MessageContent))
            {
                AddBrokenRule(new ValidationRule("MessageContent", "MessageContent_Missing"));
            }

            if (Creator == null && (CreatorId == 0 || CreatorId == null))
            {
                AddBrokenRule(new ValidationRule("Creator_CreatorId", "Creator_Null_CreatorId_Null"));
            }

            if (CreationDate == null)
            {
                CreationDate = DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Validates the message for update.
        /// </summary>
        protected override void UpdateValidate()
        {
            if (CreatorId == 0)
            {
                AddBrokenRule(new ValidationRule("CreatorId", "CreatorId_0"));
            }

            if (MessageContent != null && string.IsNullOrWhiteSpace(MessageContent))
            {
                AddBrokenRule(new ValidationRule("MessageContent", "MessageContent_Whitespace"));
            }
        }
    }
}