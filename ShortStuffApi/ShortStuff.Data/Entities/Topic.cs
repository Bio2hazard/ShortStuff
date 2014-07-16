// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Topic.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The topic.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Data.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// The topic.
    /// </summary>
    public class Topic : IDataEntity<int>
    {
        // public Topic()
        // {
        // Messages = new List<Message>();
        // Subscribers = new List<User>();
        // }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        public virtual ICollection<Message> Messages { get; set; }

        /// <summary>
        /// Gets or sets the subscribers.
        /// </summary>
        public virtual ICollection<User> Subscribers { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }
    }
}