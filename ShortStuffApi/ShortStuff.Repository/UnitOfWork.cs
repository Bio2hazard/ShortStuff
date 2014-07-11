// ShortStuff.Repository
// UnitOfWork.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using ShortStuff.Domain;
using ShortStuff.Domain.Entities;

namespace ShortStuff.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IRepository<Echo, int> _echoRepository;
        private readonly IRepository<Message, int> _messageRepository;
        private readonly IRepository<Notification, int> _notificationRepository;
        private readonly IRepository<Topic, int> _topicRepository;
        private readonly IRepository<User, decimal> _userRepository;

        public UnitOfWork(IRepository<User, decimal> userRepository, IRepository<Message, int> messageRepository, IRepository<Echo, int> echoRepository, IRepository<Notification, int> notificationRepository, IRepository<Topic, int> topicRepository)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _echoRepository = echoRepository;
            _notificationRepository = notificationRepository;
            _topicRepository = topicRepository;
        }

        public IRepository<User, decimal> UserRepository
        {
            get { return _userRepository; }
        }

        public IRepository<Message, int> MessageRepository
        {
            get { return _messageRepository; }
        }

        public IRepository<Echo, int> EchoRepository
        {
            get { return _echoRepository; }
        }

        public IRepository<Notification, int> NotificationRepository
        {
            get { return _notificationRepository; }
        }

        public IRepository<Topic, int> TopicRepository
        {
            get { return _topicRepository; }
        }
    }
}
