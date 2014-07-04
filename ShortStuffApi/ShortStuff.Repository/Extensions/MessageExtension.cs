using System.Collections.Generic;
using System.Linq;
using Omu.ValueInjecter;
using ShortStuff.Domain.Entities;
using ShortStuff.Repository.ValueInjecter;

namespace ShortStuff.Repository.Extensions
{
    public static class MessageExtension
    {
        private static bool _withCreator = false;
        private static int _creatorDepth = 0;

        private static bool _withParentMessage = false;
        private static int _parentMessageDepth = 0;

        private static bool _withReplies = false;
        private static int _repliesDepth = 0;

        private static void Reset()
        {
            _withCreator = false;
            _creatorDepth = 0;

            _withParentMessage = false;
            _parentMessageDepth = 0;

            _withReplies = false;
            _repliesDepth = 0;
        }

        public static System.Data.Entity.DbSet<ShortStuff.Data.Entities.Message> WithCreator(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.Message> messages, int depth = 0)
        {
            _withCreator = true;
            _creatorDepth = depth;
            return messages;
        }

        public static Data.Entities.Message WithCreator(this Data.Entities.Message message, int depth = 0)
        {
            _withCreator = true;
            _creatorDepth = depth;
            return message;
        }

        public static System.Data.Entity.DbSet<ShortStuff.Data.Entities.Message> WithParentMessage(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.Message> messages, int depth = 0)
        {
            _withParentMessage = true;
            _parentMessageDepth = depth;
            return messages;
        }

        public static Data.Entities.Message WithParentMessage(this Data.Entities.Message message, int depth = 0)
        {
            _withParentMessage = true;
            _parentMessageDepth = depth;
            return message;
        }
        public static System.Data.Entity.DbSet<ShortStuff.Data.Entities.Message> WithReplies(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.Message> messages, int depth = 0)
        {
            _withReplies = true;
            _repliesDepth = depth;
            return messages;
        }

        public static Data.Entities.Message WithReplies(this Data.Entities.Message message, int depth = 0)
        {
            _withReplies = true;
            _repliesDepth = depth;
            return message;
        }


        public static IEnumerable<Message> BuildMessage(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.Message> dbMessage)
        {
            if (dbMessage == null)
                return null;

            var message = dbMessage.ToList().Select(m => new Message()
            {
                Id = m.Id,
                CreationDate = m.CreationDate,
                MessageContent = m.MessageContent,
                CreatorId = m.CreatorId,
                ParentMessageId = m.ParentMessageId,
                Creator = _withCreator ? m.Creator.BuildUser() : null,
                ParentMessage = _withParentMessage ? m.ParentMessage.BuildMessage() : null,
                Replies = (_withReplies && m.Replies.Any()) ? m.Replies.InjectFromHelper(new Message(), _repliesDepth) : null,
            }).ToList();

            Reset();

            return message;
        }

        public static Message BuildMessage(this Data.Entities.Message dbMessage)
        {
            if (dbMessage == null)
                return null;

            var propertyDict = new Dictionary<string, SinglePropertyDepthInjection.PropPair>();

            propertyDict.Add("Creator", new SinglePropertyDepthInjection.PropPair { Depth = _creatorDepth, Ignored = !_withCreator });
            propertyDict.Add("ParentMessage", new SinglePropertyDepthInjection.PropPair { Depth = _parentMessageDepth, Ignored = !_withParentMessage });
            propertyDict.Add("Replies", new SinglePropertyDepthInjection.PropPair {Depth = _repliesDepth, Ignored = !_withReplies});

            var message = new Message();
            message.InjectFrom(new SinglePropertyDepthInjection(propertyDict), dbMessage);

            Reset();

            return message;
        }
    }
}
