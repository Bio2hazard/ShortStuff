using System;

namespace ShortStuff.Domain.Entities
{
    public class Echo : EntityBase<int>
    {
        public User Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public Message SourceMessage { get; set; }

        protected override void Validate()
        {
            if (Creator == null)
            {
                AddBrokenRule(new ValidationRule("Creator", "Creator_Null"));
            }

            if (SourceMessage == null)
            {
                AddBrokenRule(new ValidationRule("SourceMessage", "SourceMessage_Null"));
            }
        }
    }
}