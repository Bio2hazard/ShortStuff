using System;
using System.Collections;
using System.Collections.Generic;

namespace ShortStuff.Domain.Entities
{
    public class Message : EntityBase<int>
    {
        public User Creator { get; set; }
        public decimal? CreatorId { get; set; }
        public DateTime? CreationDate { get; set; }
        public string MessageContent { get; set; }
        public Message ParentMessage { get; set; }
        public int? ParentMessageId { get; set; }
        public IEnumerable<Message> Replies { get; set; }

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