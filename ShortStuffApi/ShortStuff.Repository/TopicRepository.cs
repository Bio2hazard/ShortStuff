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
    public class TopicRepository : RepositoryBase<Topic, int>
    {
        private readonly ShortStuffContext _context;

        public TopicRepository(ShortStuffContext context)
        {
            _context = context;
        }

        public override IEnumerable<Topic> GetAll()
        {
            return _context.Topics.BuildTopic();
        }

        public override Topic GetById(int id)
        {
            return _context.Topics.FirstOrDefault(t => t.Id == id).BuildTopic();
        }

        public override CreateStatus<int> Create(Topic entity)
        {
            if (_context.Topics.Any(t => t.Id == entity.Id))
            {
                return new CreateStatus<int>{ status = CreateStatusEnum.Conflict };
            }

            var topic = new Data.Entities.Topic();
            topic.InjectFrom<SmartConventionInjection>(entity);
            _context.Topics.Add(topic);
            _context.SaveChanges();

            return new CreateStatus<int> { status = CreateStatusEnum.Created, Id = topic.Id };
        }

        public override UpdateStatus Update(Topic entity)
        {
            var dbTopic = _context.Topics.FirstOrDefault(t => t.Id == entity.Id)
                                  .InjectFrom<NotNullInjection>(entity);

            if (dbTopic != null)
            {
                var changeTrackerTopic = _context.ChangeTracker.Entries<Data.Entities.Topic>()
                                                 .FirstOrDefault(t => t.Entity.Id == entity.Id);
            
                changeTrackerTopic.CurrentValues.SetValues(dbTopic);
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
            var topic = _context.Topics.FirstOrDefault(t => t.Id == id);
            _context.Topics.Remove(topic);
            _context.SaveChanges();
        }
    }
}
