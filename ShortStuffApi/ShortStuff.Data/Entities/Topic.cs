// ShortStuff.Data
// Topic.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System.Collections.Generic;

namespace ShortStuff.Data.Entities
{
    public class Topic : IDataEntity<int>
    {
        //public Topic()
        //{
        //    Messages = new List<Message>();
        //    Subscribers = new List<User>();
        //}

        public string Name { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<User> Subscribers { get; set; }
        public int Id { get; set; }
    }
}
