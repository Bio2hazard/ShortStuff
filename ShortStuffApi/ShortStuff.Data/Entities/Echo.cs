﻿using System;

namespace ShortStuff.Data.Entities
{
    public class Echo
    {
        public int Id { get; set; }
        public virtual User Creator { get; set; }
        public decimal CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual Message SourceMessage { get; set; }
        public int SourceMessageId { get; set; }
    }
}
