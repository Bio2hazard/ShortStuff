using System.Collections.Generic;
using System.Linq;
using Omu.ValueInjecter;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Enums;
using ShortStuff.Repository.ValueInjecter;

namespace ShortStuff.Repository.Extensions
{
    public static class NotificationExtension
    {
        private static bool _withOwner = false;
        private static int _ownerDepth = 0;

        private static bool _withSourceMessage = false;
        private static int _sourceMessageDepth = 0;

        private static bool _withSourceUser = false;
        private static int _sourceUserDepth = 0;

        private static bool _withSourceTopic = false;
        private static int _sourceTopicDepth = 0;

        private static bool _withSourceEcho = false;
        private static int _sourceEchoDepth = 0;

        private static void Reset()
        {
            _withOwner = false;
            _ownerDepth = 0;

            _withSourceMessage = false;
            _sourceMessageDepth = 0;

            _withSourceUser = false;
            _sourceUserDepth = 0;

            _withSourceTopic = false;
            _sourceTopicDepth = 0;

            _withSourceEcho = false;
            _sourceEchoDepth = 0;
        }

        public static System.Data.Entity.DbSet<ShortStuff.Data.Entities.Notification> WithOwner(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.Notification> notifications, int depth = 0)
        {
            _withOwner = true;
            _ownerDepth = depth;
            return notifications;
        }

        public static Data.Entities.Notification WithOwner(this Data.Entities.Notification notification, int depth = 0)
        {
            _withOwner = true;
            _ownerDepth = depth;
            return notification;
        }
        public static System.Data.Entity.DbSet<ShortStuff.Data.Entities.Notification> WithSourceMessage(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.Notification> notifications, int depth = 0)
        {
            _withSourceMessage = true;
            _sourceMessageDepth = depth;
            return notifications;
        }

        public static Data.Entities.Notification WithSourceMessage(this Data.Entities.Notification notification, int depth = 0)
        {
            _withSourceMessage = true;
            _sourceMessageDepth = depth;
            return notification;
        }
        public static System.Data.Entity.DbSet<ShortStuff.Data.Entities.Notification> WithSourceUser(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.Notification> notifications, int depth = 0)
        {
            _withSourceUser = true;
            _sourceUserDepth = depth;
            return notifications;
        }

        public static Data.Entities.Notification WithSourceUser(this Data.Entities.Notification notification, int depth = 0)
        {
            _withSourceUser = true;
            _sourceUserDepth = depth;
            return notification;
        }
        public static System.Data.Entity.DbSet<ShortStuff.Data.Entities.Notification> WithSourceTopic(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.Notification> notifications, int depth = 0)
        {
            _withSourceTopic = true;
            _sourceTopicDepth = depth;
            return notifications;
        }

        public static Data.Entities.Notification WithSourceTopic(this Data.Entities.Notification notification, int depth = 0)
        {
            _withSourceTopic = true;
            _sourceTopicDepth = depth;
            return notification;
        }
        public static System.Data.Entity.DbSet<ShortStuff.Data.Entities.Notification> WithSourceEcho(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.Notification> notifications, int depth = 0)
        {
            _withSourceEcho = true;
            _sourceEchoDepth = depth;
            return notifications;
        }

        public static Data.Entities.Notification WithSourceEcho(this Data.Entities.Notification notification, int depth = 0)
        {
            _withSourceEcho = true;
            _sourceEchoDepth = depth;
            return notification;
        }

        public static IEnumerable<Notification> BuildNotification(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.Notification> dbNotification)
        {
            if (dbNotification == null)
                return null;

            var notification = dbNotification.ToList().Select(n => new Notification()
            {
                Id = n.Id,
                CreationDate = n.CreationDate,
                OwnerId = n.OwnerId,
                SourceMessageId = n.SourceMessageId,
                SourceTopicId = n.SourceTopicId,
                SourceUserId = n.SourceUserId,
                SourceEchoId = n.SourceEchoId,
                NotificationStatus = (NotificationStatus)n.NotificationStatus,
                NotificationType = (NotificationType)n.NotificationType,
                Owner = _withOwner ? n.Owner.BuildUser() : null,
                SourceMessage = _withSourceMessage ? n.SourceMessage.BuildMessage() : null,
                SourceUser = _withSourceUser ? n.SourceUser.BuildUser() : null,
                SourceTopic = _withSourceTopic ? n.SourceTopic.BuildTopic() : null,
                SourceEcho = _withSourceEcho ? n.SourceEcho.BuildEcho() : null
            }).ToList();

            Reset();

            return notification;
        }

        public static Notification BuildNotification(this Data.Entities.Notification dbNotification)
        {
            if (dbNotification == null)
                return null;

            var propertyDict = new Dictionary<string, SinglePropertyDepthInjection.PropPair>();

            propertyDict.Add("Owner", new SinglePropertyDepthInjection.PropPair {Depth = _ownerDepth, Ignored = !_withOwner });
            propertyDict.Add("SourceMessage", new SinglePropertyDepthInjection.PropPair{Depth = _sourceMessageDepth, Ignored = !_withSourceMessage });
            propertyDict.Add("SourceUser", new SinglePropertyDepthInjection.PropPair{Depth = _sourceUserDepth, Ignored = !_withSourceUser });
            propertyDict.Add("SourceTopic", new SinglePropertyDepthInjection.PropPair{Depth = _sourceTopicDepth, Ignored = !_withSourceTopic });
            propertyDict.Add("SourceEcho", new SinglePropertyDepthInjection.PropPair{Depth = _sourceEchoDepth, Ignored = !_withSourceEcho });

            var notification = new Notification();
            notification.InjectFrom(new SinglePropertyDepthInjection(propertyDict), dbNotification);

            Reset();

            return notification;
        }
    }
}
