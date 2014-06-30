using System;

namespace ShortStuff.Data.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public User Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public string MessageContent { get; set; }
        public Message ParentMessage { get; set; }
    }
}
