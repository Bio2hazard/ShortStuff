// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfWork.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The unit of work.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Repository
{
    using ShortStuff.Domain;
    using ShortStuff.Domain.Entities;

    /// <summary>
    /// The unit of work.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The _echo repository.
        /// </summary>
        private readonly IRepository<Echo, int> _echoRepository;

        /// <summary>
        /// The _message repository.
        /// </summary>
        private readonly IRepository<Message, int> _messageRepository;

        /// <summary>
        /// The _notification repository.
        /// </summary>
        private readonly IRepository<Notification, int> _notificationRepository;

        /// <summary>
        /// The _topic repository.
        /// </summary>
        private readonly IRepository<Topic, int> _topicRepository;

        /// <summary>
        /// The _user repository.
        /// </summary>
        private readonly IRepository<User, decimal> _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="userRepository">
        /// The user repository.
        /// </param>
        /// <param name="messageRepository">
        /// The message repository.
        /// </param>
        /// <param name="echoRepository">
        /// The echo repository.
        /// </param>
        /// <param name="notificationRepository">
        /// The notification repository.
        /// </param>
        /// <param name="topicRepository">
        /// The topic repository.
        /// </param>
        public UnitOfWork(
            IRepository<User, decimal> userRepository, 
            IRepository<Message, int> messageRepository, 
            IRepository<Echo, int> echoRepository, 
            IRepository<Notification, int> notificationRepository, 
            IRepository<Topic, int> topicRepository)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _echoRepository = echoRepository;
            _notificationRepository = notificationRepository;
            _topicRepository = topicRepository;
        }

        /// <summary>
        /// Gets the user repository.
        /// </summary>
        public IRepository<User, decimal> UserRepository
        {
            get
            {
                return _userRepository;
            }
        }

        /// <summary>
        /// Gets the message repository.
        /// </summary>
        public IRepository<Message, int> MessageRepository
        {
            get
            {
                return _messageRepository;
            }
        }

        /// <summary>
        /// Gets the echo repository.
        /// </summary>
        public IRepository<Echo, int> EchoRepository
        {
            get
            {
                return _echoRepository;
            }
        }

        /// <summary>
        /// Gets the notification repository.
        /// </summary>
        public IRepository<Notification, int> NotificationRepository
        {
            get
            {
                return _notificationRepository;
            }
        }

        /// <summary>
        /// Gets the topic repository.
        /// </summary>
        public IRepository<Topic, int> TopicRepository
        {
            get
            {
                return _topicRepository;
            }
        }
    }
}