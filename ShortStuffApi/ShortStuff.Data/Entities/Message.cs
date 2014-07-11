// ShortStuff.Data
// Message.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System;
using System.Collections.Generic;

namespace ShortStuff.Data.Entities
{
    public class Message : IDataEntity<int>
    {
        //public Message()
        //{
        //    Replies = new List<Message>();
        //}

        public virtual User Creator { get; set; }
        public decimal CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string MessageContent { get; set; }
        public virtual Message ParentMessage { get; set; }
        public int? ParentMessageId { get; set; }
        public virtual ICollection<Message> Replies { get; set; }
        public int Id { get; set; }
    }
}
