using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using ShortStuff.Data;
using ShortStuff.Domain.Entities;

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
            return _context.Echoes.Project()
                           .To<Echo>();
        }

        public override Echo GetById(int id)
        {
            return _context.Echoes.Project()
                           .To<Echo>()
                           .FirstOrDefault(e => e.Id == id);
        }

        public override int Create(Echo entity)
        {
            var echo = AutoMapper.Mapper.Map<Data.Entities.Echo>(entity);
            _context.Echoes.Add(echo);
            _context.SaveChanges();
            return echo.Id;
        }

        public override void Update(Echo entity)
        {
            var echo = AutoMapper.Mapper.Map<Data.Entities.Echo>(entity);

            var dbEcho = _context.ChangeTracker.Entries<Data.Entities.Echo>()
                                 .FirstOrDefault(e => e.Entity.Id == echo.Id);

            if (dbEcho != null)
            {
                dbEcho.CurrentValues.SetValues(echo);
            }
            else
            {
                var tempEcho = new Data.Entities.Echo
                {
                    Id = echo.Id
                };
                _context.Echoes.Attach(tempEcho);
                _context.Entry(tempEcho).CurrentValues.SetValues(echo);
            }
            _context.SaveChanges();
        }

        public override void Delete(Echo entity)
        {
            var echo = _context.Echoes.FirstOrDefault(e => e.Id == entity.Id);
            _context.Echoes.Remove(echo);
            _context.SaveChanges();
        }
    }
}
