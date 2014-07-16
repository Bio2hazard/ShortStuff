// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Echo.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The echo business model which is equivalent to the echo data entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Domain.Entities
{
    using System;

    /// <summary>
    /// The echo business model which is equivalent to the echo data entity.
    /// </summary>
    public class Echo : EntityBase<int>
    {
        /// <summary>
        /// Gets or sets the creator of the echo.
        /// </summary>
        public User Creator { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the creator of the echo.
        /// </summary>
        public decimal? CreatorId { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the echo.
        /// </summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the message being echoed.
        /// </summary>
        public Message SourceMessage { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the message being echoed.
        /// </summary>
        public int? SourceMessageId { get; set; }

        /// <summary>
        /// Validates the echo for creation.
        /// </summary>
        protected override void Validate()
        {
            if (Creator == null && (CreatorId == 0 || CreatorId == null))
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

        /// <summary>
        /// Validates the echo for update.
        /// </summary>
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