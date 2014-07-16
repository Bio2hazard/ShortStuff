// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Message.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The message.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Data.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The message.
    /// </summary>
    public class Message : IDataEntity<int>
    {
        // public Message()
        // {
        // Replies = new List<Message>();
        // }

        /// <summary>
        /// Gets or sets the creator.
        /// </summary>
        public virtual User Creator { get; set; }

        /// <summary>
        /// Gets or sets the creator id.
        /// </summary>
        public decimal CreatorId { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the message content.
        /// </summary>
        public string MessageContent { get; set; }

        /// <summary>
        /// Gets or sets the parent message.
        /// </summary>
        public virtual Message ParentMessage { get; set; }

        /// <summary>
        /// Gets or sets the parent message id.
        /// </summary>
        public int? ParentMessageId { get; set; }

        /// <summary>
        /// Gets or sets the replies.
        /// </summary>
        public virtual ICollection<Message> Replies { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }
    }
}