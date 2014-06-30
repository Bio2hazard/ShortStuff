using ShortStuff.Domain.Entities;

namespace ShortStuff.Domain
{
    public interface IUnitOfWork
    {
        IRepository<User, string> UserRepository { get; }
        IRepository<Message, int> MessageRepository { get; }
        IRepository<Echo, int> EchoRepository { get; }
        IRepository<Notification, int> NotificationRepository { get; }
        IRepository<Topic, int> TopicRepository { get; }
    }
}
