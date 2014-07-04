using System;

namespace ShortStuff.Domain.Entities
{
    public class Echo : EntityBase<int>
    {
        public User Creator { get; set; }
        public decimal? CreatorId { get; set; }
        public DateTime? CreationDate { get; set; }
        public Message SourceMessage { get; set; }
        public int? SourceMessageId { get; set; }

        protected override void Validate()
        {
            if (Creator == null && ( CreatorId == 0 || CreatorId == null))
            {
                AddBrokenRule(new ValidationRule("Creator_CreatorId", "Creator_Null_CreatorId_Null"));
            }
            if (SourceMessage == null && (SourceMessageId == 0 || SourceMessageId == null))
            {
                AddBrokenRule(new ValidationRule("SourceMessage_SourceMessageId", "SourceMessageId_Null_SourceMessageId_Null"));
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
            if (SourceMessageId == 0)
            {
                AddBrokenRule(new ValidationRule("SourceMessageId", "SourceMessageId_0"));
            }
        }
    }
}