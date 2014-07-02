using System.Collections.Generic;
using System.Linq;
using ShortStuff.Data;
using ShortStuff.Domain.Entities;

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
            //return _context.Notifications.Project()
            //               .To<Notification>();
            return null;
        }

        public override Notification GetById(int id)
        {
            //return _context.Notifications.Project()
            //               .To<Notification>()
            //               .FirstOrDefault(n => n.Id == id);
            return null;
        }

        public override int Create(Notification entity)
        {
            //var notification = AutoMapper.Mapper.Map<Data.Entities.Notification>(entity);
            //_context.Notifications.Add(notification);
            //_context.SaveChanges();
            //return notification.Id;
            return 0;
        }

        public override void Update(Notification entity)
        {
            //var notification = AutoMapper.Mapper.Map<Data.Entities.Notification>(entity);

            //var dbNotification = _context.ChangeTracker.Entries<Data.Entities.Notification>()
            //                             .FirstOrDefault(n => n.Entity.Id == notification.Id);

            //if (dbNotification != null)
            //{
            //    dbNotification.CurrentValues.SetValues(notification);
            //}
            //else
            //{
            //    var tempNotification = new Data.Entities.Notification
            //    {
            //        Id = notification.Id
            //    };
            //    _context.Notifications.Attach(tempNotification);
            //    _context.Entry(tempNotification)
            //            .CurrentValues.SetValues(notification);
            //}
            //_context.SaveChanges();
        }

        public override void Delete(Notification entity)
        {
            var notification = _context.Notifications.FirstOrDefault(n => n.Id == entity.Id);
            _context.Notifications.Remove(notification);
            _context.SaveChanges();
        }
    }
}
