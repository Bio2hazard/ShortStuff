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

        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<User> Followers { get; set; }
        public virtual ICollection<Message> Favorites { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Topic> SubscribedTopics { get; set; }

    }
}
