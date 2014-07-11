// ShortStuff.Data
// Echo.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System;

namespace ShortStuff.Data.Entities
{
    public class Echo : IDataEntity<int>
    {
        public virtual User Creator { get; set; }
        public decimal CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual Message SourceMessage { get; set; }
        public int SourceMessageId { get; set; }
        public int Id { get; set; }
    }
}
