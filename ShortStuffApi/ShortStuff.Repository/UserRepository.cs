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
    public class UserRepository : RepositoryBase<User, decimal>
    {
        private readonly ShortStuffContext _context;

        public UserRepository(ShortStuffContext context)
        {
            _context = context;
        }

        public override IEnumerable<User> GetAll()
        {
            return _context.Users.BuildUser();
        }

        public override User GetById(decimal id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id).BuildUser();
        }

        public override CreateStatus<decimal> Create(User entity)
        {
            if (_context.Users.Any(u => u.Id == entity.Id))
            {
                return new CreateStatus<decimal> { status = CreateStatusEnum.Conflict };
            }

            var user = new Data.Entities.User();
            user.InjectFrom<SmartConventionInjection>(entity);
            _context.Users.Add(user);
            _context.SaveChanges();

            return new CreateStatus<decimal> { status = CreateStatusEnum.Created, Id = user.Id };
        }

        public override UpdateStatus Update(User entity)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.Id == entity.Id)
                                 .InjectFrom<NotNullInjection>(entity);

            if (dbUser != null)
            {
                var changeTrackerUser = _context.ChangeTracker.Entries<Data.Entities.User>()
                                .FirstOrDefault(u => u.Entity.Id == entity.Id);

                changeTrackerUser.CurrentValues.SetValues(dbUser);
                if (_context.SaveChanges() == 0)
                {
                    return UpdateStatus.NoChange;
                }
                return UpdateStatus.Updated;
            }
            return UpdateStatus.NotFound;
        }

        public override void Delete(decimal id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
