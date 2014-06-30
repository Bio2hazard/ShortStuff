using System.Collections.Generic;

namespace ShortStuff.Data.Entities
{
    public class Topic
    {
        public Topic()
        {
            Messages = new List<Message>();
            Subscribers = new List<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<User> Subscribers { get; set; }
    }
}
