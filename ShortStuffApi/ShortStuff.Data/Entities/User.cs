// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The user.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Data.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// The user.
    /// </summary>
    public class User : IDataEntity<decimal>
    {
        // public User()
        // {
        // Messages = new List<Message>();
        // Echoes = new List<Echo>();
        // Followers = new List<User>();
        // Following = new List<User>();
        // Favorites = new List<Message>();
        // Notifications = new List<Notification>();
        // SubscribedTopics = new List<Topic>();
        // }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the picture.
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        public virtual ICollection<Message> Messages { get; set; }

        /// <summary>
        /// Gets or sets the echoes.
        /// </summary>
        public virtual ICollection<Echo> Echoes { get; set; }

        /// <summary>
        /// Gets or sets the followers.
        /// </summary>
        public virtual ICollection<User> Followers { get; set; }

        /// <summary>
        /// Gets or sets the following.
        /// </summary>
        public virtual ICollection<User> Following { get; set; }

        /// <summary>
        /// Gets or sets the favorites.
        /// </summary>
        public virtual ICollection<Message> Favorites { get; set; }

        /// <summary>
        /// Gets or sets the notifications.
        /// </summary>
        public virtual ICollection<Notification> Notifications { get; set; }

        /// <summary>
        /// Gets or sets the subscribed topics.
        /// </summary>
        public virtual ICollection<Topic> SubscribedTopics { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public decimal Id { get; set; }
    }
}