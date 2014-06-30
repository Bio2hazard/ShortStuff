using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using ShortStuff.Data;
using ShortStuff.Domain.Entities;

namespace ShortStuff.Repository
{
    public class MessageRepository : RepositoryBase<Message, int>
    {
        private readonly ShortStuffContext _context;

        public MessageRepository(ShortStuffContext context)
        {
            _context = context;
        }

        public override IEnumerable<Message> GetAll()
        {
            return _context.Messages.Project()
                           .To<Message>();
        }

        public override Message GetById(int id)
        {
            return _context.Messages.Project()
                           .To<Message>()
                           .FirstOrDefault(m => m.Id == id);
        }

        public override int Create(Message entity)
        {
            var message = AutoMapper.Mapper.Map<Data.Entities.Message>(entity);
            _context.Messages.Add(message);
            _context.SaveChanges();
            return message.Id;
        }

        public override void Update(Message entity)
        {
            var message = AutoMapper.Mapper.Map<Data.Entities.Message>(entity);
            var dbMessage = _context.ChangeTracker.Entries<Data.Entities.Message>()
                                    .FirstOrDefault(m => m.Entity.Id == message.Id);
            if (dbMessage != null)
            {
                dbMessage.CurrentValues.SetValues(message);
            }
            else
            {
                var tempMessage = new Data.Entities.Message
                {
                    Id = message.Id
                };
                _context.Messages.Attach(tempMessage);
                _context.Entry(tempMessage).CurrentValues.SetValues(message);
            }
            _context.SaveChanges();
        }

        public override void Delete(Message entity)
        {
            var message = _context.Messages.FirstOrDefault(m => m.Id == entity.Id);
            _context.Messages.Remove(message);
            _context.SaveChanges();
        }
    }
}
