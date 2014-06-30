using System;

namespace ShortStuff.Data.Entities
{
    public class Echo
    {
        public int Id { get; set; }
        public User Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public Message SourceMessage { get; set; }
    }
}
