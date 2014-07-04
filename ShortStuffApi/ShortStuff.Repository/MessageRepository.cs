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
    public class MessageRepository : RepositoryBase<Message, int>
    {
        private readonly ShortStuffContext _context;

        public MessageRepository(ShortStuffContext context)
        {
            _context = context;
        }

        public override IEnumerable<Message> GetAll()
        {
            return _context.Messages.BuildMessage();
        }

        public override Message GetById(int id)
        {
            return _context.Messages.FirstOrDefault(m => m.Id == id).BuildMessage();
        }

        public override CreateStatus<int> Create(Message entity)
        {
            if (_context.Messages.Any(m => m.Id == entity.Id))
            {
                return new CreateStatus<int> { status = CreateStatusEnum.Conflict };
            }
            var message = new Data.Entities.Message();
            message.InjectFrom<SmartConventionInjection>(entity);
            _context.Messages.Add(message);
            _context.SaveChanges();

            return new CreateStatus<int>{status = CreateStatusEnum.Created, Id = message.Id };
        }

        public override UpdateStatus Update(Message entity)
        {
            var dbMessage = _context.Messages.FirstOrDefault(m => m.Id == entity.Id)
                                 .InjectFrom<NotNullInjection>(entity);

            if (dbMessage != null)
            {
                var changeTrackerMessage = _context.ChangeTracker.Entries<Data.Entities.Message>()
                                .FirstOrDefault(m => m.Entity.Id == entity.Id);

                changeTrackerMessage.CurrentValues.SetValues(dbMessage);
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
            var message = _context.Messages.FirstOrDefault(m => m.Id == id);
            _context.Messages.Remove(message);
            _context.SaveChanges();
        }
    }
}
