// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The UnitOfWork interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// todo: This is not a unit of work, rename it to something that makes more sense.
namespace ShortStuff.Domain
{
    using ShortStuff.Domain.Entities;

    /// <summary>
    /// The UnitOfWork interface.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the user repository.
        /// </summary>
        IRepository<User, decimal> UserRepository { get; }

        /// <summary>
        /// Gets the message repository.
        /// </summary>
        IRepository<Message, int> MessageRepository { get; }

        /// <summary>
        /// Gets the echo repository.
        /// </summary>
        IRepository<Echo, int> EchoRepository { get; }

        /// <summary>
        /// Gets the notification repository.
        /// </summary>
        IRepository<Notification, int> NotificationRepository { get; }

        /// <summary>
        /// Gets the topic repository.
        /// </summary>
        IRepository<Topic, int> TopicRepository { get; }
    }
}