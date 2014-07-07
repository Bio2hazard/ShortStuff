using ShortStuff.Data;
using ShortStuff.Domain.Entities;

namespace ShortStuff.Repository
{
    public class NotificationRepository : RepositoryBase<Data.Entities.Notification, Notification, int>
    {
        public NotificationRepository(ShortStuffContext context) : base(context)
        {
        }
    }
}
