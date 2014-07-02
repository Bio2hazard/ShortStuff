using System.Collections.Generic;
using System.Linq;
using ShortStuff.Data;
using ShortStuff.Domain.Entities;

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
            //return _context.Topics.Project()
            //               .To<Topic>();
            return null;
        }

        public override Topic GetById(int id)
        {
            //return _context.Topics.Project()
            //               .To<Topic>()
            //               .FirstOrDefault(t => t.Id == id);
            return null;
        }

        public override int Create(Topic entity)
        {
            //var topic = AutoMapper.Mapper.Map<Data.Entities.Topic>(entity);
            //_context.Topics.Add(topic);
            //_context.SaveChanges();
            //return topic.Id;
            return 0;
        }

        public override void Update(Topic entity)
        {
            //var topic = AutoMapper.Mapper.Map<Data.Entities.Topic>(entity);

            //var dbTopic = _context.ChangeTracker.Entries<Data.Entities.Topic>()
            //                      .FirstOrDefault(t => t.Entity.Id == topic.Id);

            //if (dbTopic != null)
            //{
            //    dbTopic.CurrentValues.SetValues(topic);
            //}
            //else
            //{
            //    var tempTopic = new Data.Entities.Topic
            //    {
            //        Id = topic.Id
            //    };
            //    _context.Topics.Attach(tempTopic);
            //    _context.Entry(tempTopic).CurrentValues.SetValues(topic);
            //}
            //_context.SaveChanges();
        }

        public override void Delete(Topic entity)
        {
            var topic = _context.Topics.FirstOrDefault(t => t.Id == entity.Id);
            _context.Topics.Remove(topic);
            _context.SaveChanges();
        }
    }
}
