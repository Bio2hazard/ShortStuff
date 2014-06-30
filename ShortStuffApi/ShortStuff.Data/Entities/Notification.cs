using System;

namespace ShortStuff.Data.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public User Owner { get; set; }
        public DateTime CreationDate { get; set; }
        public int NotificationType { get; set; }
        public Message SourceMessage { get; set; }
        public User SourceUser { get; set; }
        public Topic SourceTopic { get; set; }
        public int NotificationStatus { get; set; }

    }
}
