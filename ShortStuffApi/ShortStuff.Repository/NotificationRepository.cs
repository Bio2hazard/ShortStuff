using System.Collections.Generic;
using System.Linq;
using Omu.ValueInjecter;
using ShortStuff.Data;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Enums;
using ShortStuff.Repository.Extensions;
using ShortStuff.Repository.ValueInjecter;

namespace ShortStuff.Repository
{
    public class NotificationRepository : RepositoryBase<Notification, int>
    {
        private readonly ShortStuffContext _context;

        public NotificationRepository(ShortStuffContext context)
        {
            _context = context;
        }

        public override IEnumerable<Notification> GetAll()
        {
            return _context.Notifications.BuildNotification();
        }

        public override Notification GetById(int id)
        {
            return _context.Notifications.FirstOrDefault(n => n.Id == id).BuildNotification();
        }

        public override CreateStatus<int> Create(Notification entity)
        {
            if (_context.Notifications.Any(n => n.Id == entity.Id))
            {
                return new CreateStatus<int> { status = CreateStatusEnum.Conflict };
            }

            var notification = new Data.Entities.Notification();
            notification.InjectFrom<SmartConventionInjection>(entity);
            _context.Notifications.Add(notification);
            _context.SaveChanges();

            return new CreateStatus<int> { status = CreateStatusEnum.Created, Id = notification.Id };
        }

        public override UpdateStatus Update(Notification entity)
        {
            var dbNotification = _context.Notifications.FirstOrDefault(n => n.Id == entity.Id)
                                 .InjectFrom<NotNullInjection>(entity);

            if (dbNotification != null)
            {
                var changeTrackerNotification = _context.ChangeTracker.Entries<Data.Entities.Notification>()
                                .FirstOrDefault(n => n.Entity.Id == entity.Id);

                changeTrackerNotification.CurrentValues.SetValues(dbNotification);
                if (_context.SaveChanges() == 0)
                {
                    return UpdateStatus.NoChange;
                }
                return UpdateStatus.Updated;
            }
            return UpdateStatus.NotFound;
        }

        public override void Delete(int id)
        {
            var notification = _context.Notifications.FirstOrDefault(n => n.Id == id);
            _context.Notifications.Remove(notification);
            _context.SaveChanges();
        }
    }
}
