using System.Collections.Generic;
using System.Linq;
using Omu.ValueInjecter;
using ShortStuff.Domain.Entities;
using ShortStuff.Repository.ValueInjecter;

namespace ShortStuff.Repository.Extensions
{
    public static class EchoExtension
    {
        private static bool _withCreator = false;
        private static int _creatorDepth = 0;

        private static bool _withSourceMessage = false;
        private static int _sourceMessageDepth = 0;

        private static void Reset()
        {
            _withCreator = false;
            _creatorDepth = 0;

            _withSourceMessage = false;
            _sourceMessageDepth = 0;
        }

        public static System.Data.Entity.DbSet<ShortStuff.Data.Entities.Echo> WithCreator(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.Echo> echoes, int depth = 0)
        {
            _withCreator = true;
            _creatorDepth = depth;
            return echoes;
        }

        public static Data.Entities.Echo WithCreator(this Data.Entities.Echo echo, int depth = 0)
        {
            _withCreator = true;
            _creatorDepth = depth;
            return echo;
        }

        public static System.Data.Entity.DbSet<ShortStuff.Data.Entities.Echo> WithSourceMessage(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.Echo> echoes, int depth = 0)
        {
            _withSourceMessage = true;
            _sourceMessageDepth = depth;
            return echoes;
        }

        public static Data.Entities.Echo WithSourceMessage(this Data.Entities.Echo echo, int depth = 0)
        {
            _withSourceMessage = true;
            _sourceMessageDepth = depth;
            return echo;
        }

        public static IEnumerable<Echo> BuildEcho(this System.Data.Entity.DbSet<ShortStuff.Data.Entities.Echo> dbEcho)
        {
            if (dbEcho == null)
                return null;

            var echo = dbEcho.ToList().Select(e => new Echo()
            {
                Id = e.Id,
                CreationDate = e.CreationDate,
                CreatorId = e.CreatorId,
                SourceMessageId = e.SourceMessageId,
                Creator = _withCreator ? e.Creator.BuildUser() : null,
                SourceMessage = _withSourceMessage ? e.SourceMessage.BuildMessage() : null
            }).ToList();

            Reset();

            return echo;
        }

        public static Echo BuildEcho(this Data.Entities.Echo dbEcho)
        {
            if (dbEcho == null)
                return null;

            var propertyDict = new Dictionary<string, SinglePropertyDepthInjection.PropPair>();

            propertyDict.Add("Creator", new SinglePropertyDepthInjection.PropPair { Depth = _creatorDepth, Ignored = !_withCreator });
            propertyDict.Add("SourceMessage", new SinglePropertyDepthInjection.PropPair { Depth = _sourceMessageDepth, Ignored = !_withSourceMessage});

            var echo = new Echo();
            echo.InjectFrom(new SinglePropertyDepthInjection(propertyDict), dbEcho);

            Reset();

            return echo;
        }
    }
}
