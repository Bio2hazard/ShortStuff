// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Notification.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The notification.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Data.Entities
{
    using System;

    /// <summary>
    /// The notification.
    /// </summary>
    public class Notification : IDataEntity<int>
    {
        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        public virtual User Owner { get; set; }

        /// <summary>
        /// Gets or sets the owner id.
        /// </summary>
        public decimal OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the notification type.
        /// </summary>
        public int NotificationType { get; set; }

        /// <summary>
        /// Gets or sets the source message.
        /// </summary>
        public virtual Message SourceMessage { get; set; }

        /// <summary>
        /// Gets or sets the source message id.
        /// </summary>
        public int? SourceMessageId { get; set; }

        /// <summary>
        /// Gets or sets the source user.
        /// </summary>
        public virtual User SourceUser { get; set; }

        /// <summary>
        /// Gets or sets the source user id.
        /// </summary>
        public decimal? SourceUserId { get; set; }

        /// <summary>
        /// Gets or sets the source topic.
        /// </summary>
        public virtual Topic SourceTopic { get; set; }

        /// <summary>
        /// Gets or sets the source topic id.
        /// </summary>
        public int? SourceTopicId { get; set; }

        /// <summary>
        /// Gets or sets the source echo.
        /// </summary>
        public virtual Echo SourceEcho { get; set; }

        /// <summary>
        /// Gets or sets the source echo id.
        /// </summary>
        public int? SourceEchoId { get; set; }

        /// <summary>
        /// Gets or sets the notification status.
        /// </summary>
        public int NotificationStatus { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }
    }
}