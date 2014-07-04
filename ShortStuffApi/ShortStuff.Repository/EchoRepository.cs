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
    public class EchoRepository : RepositoryBase<Echo, int>
    {
        private readonly ShortStuffContext _context;

        public EchoRepository(ShortStuffContext context)
        {
            _context = context;
        }

        public override IEnumerable<Echo> GetAll()
        {
            return _context.Echoes.BuildEcho();
        }

        public override Echo GetById(int id)
        {
            return _context.Echoes.FirstOrDefault(e => e.Id == id).BuildEcho();
        }

        public override CreateStatus<int> Create(Echo entity)
        {
            if (_context.Echoes.Any(e => e.Id == entity.Id))
            {
                return new CreateStatus<int> { status = CreateStatusEnum.Conflict };
            }
            var echo = new Data.Entities.Echo();
            echo.InjectFrom<SmartConventionInjection>(entity);
            _context.Echoes.Add(echo);
            _context.SaveChanges();

            return new CreateStatus<int> { status = CreateStatusEnum.Created, Id = echo.Id };
        }

        public override UpdateStatus Update(Echo entity)
        {
            var dbEcho = _context.Echoes.FirstOrDefault(e => e.Id == entity.Id)
                                 .InjectFrom<NotNullInjection>(entity);

            if (dbEcho != null)
            {
                var changeTrackerEcho = _context.ChangeTracker.Entries<Data.Entities.Echo>()
                                .FirstOrDefault(e => e.Entity.Id == entity.Id);

                changeTrackerEcho.CurrentValues.SetValues(dbEcho);
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
            var echo = _context.Echoes.FirstOrDefault(e => e.Id == id);
            _context.Echoes.Remove(echo);
            _context.SaveChanges();
        }
    }
}
