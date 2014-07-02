using System;

namespace ShortStuff.Data.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public virtual User Owner { get; set; }
        public DateTime CreationDate { get; set; }
        public int NotificationType { get; set; }
        public virtual Message SourceMessage { get; set; }
        public virtual User SourceUser { get; set; }
        public virtual Topic SourceTopic { get; set; }
        public int NotificationStatus { get; set; }

    }
}
