using System;

namespace ShortStuff.Domain.Entities
{
    public class Message : EntityBase<int>
    {
        public User Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public string MessageContent { get; set; }
        public Message ParentMessage { get; set; }

        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(MessageContent))
            {
                AddBrokenRule(new ValidationRule("MessageContent", "MessageContent_Missing"));
            }
            if (Creator == null)
            {
                AddBrokenRule(new ValidationRule("Creator", "Creator_Null"));
            }
        }
    }
}