using System.Collections.Generic;
using System.Linq;
using Omu.ValueInjecter;
using ShortStuff.Domain.Entities;
using ShortStuff.Repository.ValueInjecter;

namespace ShortStuff.Repository.Extensions
{
    public static class TopicExtension
    {
        private static bool _withMessages = false;
        private static int _messagesDepth = 0;

        private static bool _withSubscribers = false;
        private static int _subscribersDepth = 0;

        private static void Reset()
        {
            _withMessages = false;
            _messagesDepth = 0;

            _withSubscribers = false;
            _subscribersDepth = 0;
        }

        public static System.Data.Entity.DbSet<ShortStuff.Data.Entities.Topic> WithMessages(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.Topic> topics, int depth = 0)
        {
            _withMessages = true;
            _messagesDepth = depth;
            return topics;
        }

        public static Data.Entities.Topic WithMessages(this Data.Entities.Topic topic, int depth = 0)
        {
            _withMessages = true;
            _messagesDepth = depth;
            return topic;
        }

        public static System.Data.Entity.DbSet<ShortStuff.Data.Entities.Topic> WithSubscribers(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.Topic> topics, int depth = 0)
        {
            _withSubscribers = true;
            _subscribersDepth = depth;
            return topics;
        }

        public static Data.Entities.Topic WithSubscribers(this Data.Entities.Topic topic, int depth = 0)
        {
            _withSubscribers = true;
            _subscribersDepth = depth;
            return topic;
        }

        public static IEnumerable<Topic> BuildTopic(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.Topic> dbTopic)
        {
            if (dbTopic == null)
                return null;

            var topic = dbTopic.ToList().Select(t => new Topic()
            {
                Id = t.Id,
                Name = t.Name,
                Messages = (_withMessages && t.Messages.Any()) ? t.Messages.InjectFromHelper(new Message(), _messagesDepth) : null,
                Subscribers = (_withSubscribers && t.Subscribers.Any()) ? t.Subscribers.InjectFromHelper(new User(), _subscribersDepth) : null
            }).ToList();

            Reset();

            return topic;
        }

        public static Topic BuildTopic(this Data.Entities.Topic dbTopic)
        {
            if (dbTopic == null)
                return null;

            var propertyDict = new Dictionary<string, SinglePropertyDepthInjection.PropPair>();

            propertyDict.Add("Messages", new SinglePropertyDepthInjection.PropPair { Depth = _messagesDepth, Ignored = !_withMessages });
            propertyDict.Add("Subscribers", new SinglePropertyDepthInjection.PropPair { Depth = _subscribersDepth, Ignored = !_withSubscribers });

            var topic = new Topic();
            topic.InjectFrom(new SinglePropertyDepthInjection(propertyDict), dbTopic);

            Reset();

            return topic;
        }
    }
}
