using System.Collections.Generic;

namespace ShortStuff.Data.Entities
{
    public class User
    {
        public User()
        {
            Messages = new List<Message>();
            Followers = new List<User>();
            Favorites = new List<Message>();
            Notifications = new List<Notification>();
            SubscribedTopics = new List<Topic>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tag { get; set; }
        public string Picture { get; set; }

        public ICollection<Message> Messages { get; set; }
        public ICollection<User> Followers { get; set; }
        public ICollection<Message> Favorites { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Topic> SubscribedTopics { get; set; }

    }
}
