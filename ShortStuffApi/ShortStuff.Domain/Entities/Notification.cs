// ShortStuff.Domain
// Notification.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System;
using ShortStuff.Domain.Enums;

namespace ShortStuff.Domain.Entities
{
    public class Notification : EntityBase<int>
    {
        public User Owner { get; set; }
        public decimal? OwnerId { get; set; }
        public DateTime? CreationDate { get; set; }
        public NotificationType NotificationType { get; set; }
        public Message SourceMessage { get; set; }
        public int? SourceMessageId { get; set; }
        public User SourceUser { get; set; }
        public decimal? SourceUserId { get; set; }
        public Topic SourceTopic { get; set; }
        public int? SourceTopicId { get; set; }
        public Echo SourceEcho { get; set; }
        public int? SourceEchoId { get; set; }
        public NotificationStatus NotificationStatus { get; set; }

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

        protected override void UpdateValidate()
        {
            if (OwnerId == 0)
            {
                AddBrokenRule(new ValidationRule("OwnerId", "OwnerId_0"));
            }
        }

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

        private void AddBrokenNotificationTypeRule<T>(NotificationType type, T requirement, decimal? requirementId)
        {
            // ReSharper disable once CompareNonConstrainedGenericWithNull
            if (requirement == null && (requirementId == 0 || requirementId == null))
            {
                AddBrokenRule(new ValidationRule("NotificationType" + type, "NotificationType_Required" + typeof (T).Name + "Null"));
            }
        }
    }
}
