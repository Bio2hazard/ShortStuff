// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Notification.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The notification business model which is equivalent to the notification data entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Domain.Entities
{
    using System;

    using ShortStuff.Domain.Enums;

    /// <summary>
    /// The notification business model which is equivalent to the notification data entity.
    /// </summary>
    public class Notification : EntityBase<int>
    {
        /// <summary>
        /// Gets or sets the owner of the notification.
        /// </summary>
        public User Owner { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the owner of the notification.
        /// </summary>
        public decimal? OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the notification.
        /// </summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="NotificationType"/> of the notification.
        /// </summary>
        public NotificationType NotificationType { get; set; }

        /// <summary>
        /// Gets or sets the message directly related to this notification.
        /// </summary>
        public Message SourceMessage { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the message directly related to this notification.
        /// </summary>
        public int? SourceMessageId { get; set; }

        /// <summary>
        /// Gets or sets the user directly related to this notification.
        /// </summary>
        public User SourceUser { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the user directly related to this notification.
        /// </summary>
        public decimal? SourceUserId { get; set; }

        /// <summary>
        /// Gets or sets the topic directly related to this notification.
        /// </summary>
        public Topic SourceTopic { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the topic directly related to this notification.
        /// </summary>
        public int? SourceTopicId { get; set; }

        /// <summary>
        /// Gets or sets the echo directly related to this notification.
        /// </summary>
        public Echo SourceEcho { get; set; }

        /// <summary>
        /// Gets or sets the unique id of the echo directly related to this notification.
        /// </summary>
        public int? SourceEchoId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="NotificationStatus"/> of the notification.
        /// </summary>
        public NotificationStatus NotificationStatus { get; set; }

        /// <summary>
        /// Validates the notification for creation.
        /// </summary>
        protected override void Validate()
        {
            if (Owner == null && (OwnerId == 0 || OwnerId == null))
            {
                AddBrokenRule(new ValidationRule("Owner_OwnerId", "Owner_Null_OwnerId_Null"));
            }

            if (CreationDate == null)
            {
                CreationDate = DateTime.UtcNow;
            }

            ValidateNotificationType(NotificationType);
        }

        /// <summary>
        /// Validates the notification for update.
        /// </summary>
        protected override void UpdateValidate()
        {
            // todo: NotificationType validation should be applied to updates as well, but is hard to implement with partial updates. Will tackle later.
            if (OwnerId == 0)
            {
                AddBrokenRule(new ValidationRule("OwnerId", "OwnerId_0"));
            }
        }

        /// <summary>
        /// Validates the notification based on it's NotificationType.
        /// </summary>
        /// <param name="type">
        /// The <see cref="NotificationType"/> by which to validate the notification.
        /// </param>
        private void ValidateNotificationType(NotificationType type)
        {
            switch (type)
            {
                    // Make sure SourceUser requirement is met
                case NotificationType.NewFollower:
                case NotificationType.LostFollower:
                    AddBrokenNotificationTypeRule(type, SourceUser, SourceUserId);
                    break;

                    // Make sure SourceUser + SourceMessage requirement is met
                case NotificationType.FollowedNewMessage:
                case NotificationType.NewMention:
                    AddBrokenNotificationTypeRule(type, SourceUser, SourceUserId);
                    AddBrokenNotificationTypeRule(type, SourceMessage, SourceMessageId);
                    break;

                    // Make sure SourceUser + SourceEcho requirement is met
                case NotificationType.FollowedNewEcho:
                    AddBrokenNotificationTypeRule(type, SourceUser, SourceUserId);
                    AddBrokenNotificationTypeRule(type, SourceEcho, SourceEchoId);
                    break;

                    // Make sure SourceUser + SourceTopic + SourceMessage requirement is met
                case NotificationType.TopicNewMessage:
                    AddBrokenNotificationTypeRule(type, SourceUser, SourceUserId);
                    AddBrokenNotificationTypeRule(type, SourceMessage, SourceMessageId);
                    AddBrokenNotificationTypeRule(type, SourceTopic, SourceTopicId);
                    break;
            }
        }

        /// <summary>
        /// Checks a requirement and adds a automatically generated ValidationRule 
        /// for the NotificationType if the requirement is not met.
        /// </summary>
        /// <param name="type">
        /// The NotificationType related to this this rule check.
        /// </param>
        /// <param name="requirement">
        /// The business model requirement of this rule.
        /// </param>
        /// <param name="requirementId">
        /// The business model requirement identified by it's id.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        private void AddBrokenNotificationTypeRule<T>(NotificationType type, T requirement, decimal? requirementId)
        {
            // ReSharper disable once CompareNonConstrainedGenericWithNull
            if (requirement == null && (requirementId == 0 || requirementId == null))
            {
                AddBrokenRule(new ValidationRule("NotificationType" + type, "NotificationType_Required" + typeof(T).Name + "Null"));
            }
        }
    }
}