using System;
using System.Collections.Generic;

namespace ShortStuff.Data.Entities
{
    public class Message
    {
        public Message()
        {
            Replies = new List<Message>();
        }

        public int Id { get; set; }
        public virtual User Creator { get; set; }
        public decimal CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string MessageContent { get; set; }
        public virtual Message ParentMessage { get; set; }
        public int? ParentMessageId { get; set; }
        public virtual ICollection<Message> Replies { get; set; }
    }
}
