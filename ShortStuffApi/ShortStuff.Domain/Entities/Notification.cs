using System;
using ShortStuff.Domain.Enums;

namespace ShortStuff.Domain.Entities
{
    public class Notification : EntityBase<int>
    {
        public User Owner { get; set; }
        public DateTime CreationDate { get; set; }
        public NotificationType NotificationType { get; set; }
        public Message SourceMessage { get; set; }
        public User SourceUser { get; set; }
        public Topic SourceTopic { get; set; }
        public NotificationStatus NotificationStatus { get; set; }

        protected override void Validate()
        {
            if (Owner == null)
            {
                AddBrokenRule(new ValidationRule("Owner", "Owner_Null"));
            }

            ValidateNotificationType(NotificationType);
        }

        private void ValidateNotificationType(NotificationType type)
        {
            switch (type)
            {
                // Make sure SourceUser requirement is met
                case NotificationType.NewFollower:
                case NotificationType.LostFollower:
                    AddBrokenNotificationTypeRule(type, SourceUser);
                    break; 

                // Make sure SourceUser + SourceMessage requirement is met
                case NotificationType.FollowedNewMessage:
                case NotificationType.NewMention:
                case NotificationType.FollowedNewEcho:
                    AddBrokenNotificationTypeRule(type, SourceUser);
                    AddBrokenNotificationTypeRule(type, SourceMessage);
                    break;

                // Make sure SourceUser + SourceTopic + SourceMessage requirement is met
                case NotificationType.TopicNewMessage:
                    AddBrokenNotificationTypeRule(type, SourceUser);
                    AddBrokenNotificationTypeRule(type, SourceMessage);
                    AddBrokenNotificationTypeRule(type, SourceTopic);
                    break;
            }
        }

        private void AddBrokenNotificationTypeRule(NotificationType type, object requirement)
        {
            if (requirement == null)
            {
                AddBrokenRule(new ValidationRule("NotificationType" + type, "NotificationType_Required" + requirement.GetType() + "Null"));
            }
        }

    }
}