﻿using System;

namespace ShortStuff.Data.Entities
{
    public class Notification : IDataEntity<int>
    {
        public int Id { get; set; }
        public virtual User Owner { get; set; }
        public decimal OwnerId { get; set; }
        public DateTime CreationDate { get; set; }
        public int NotificationType { get; set; }
        public virtual Message SourceMessage { get; set; }
        public int? SourceMessageId { get; set; }
        public virtual User SourceUser { get; set; }
        public decimal? SourceUserId { get; set; }
        public virtual Topic SourceTopic { get; set; }
        public int? SourceTopicId { get; set; }
        public virtual Echo SourceEcho { get; set; }
        public int? SourceEchoId { get; set; }
        public int NotificationStatus { get; set; }

    }
}
