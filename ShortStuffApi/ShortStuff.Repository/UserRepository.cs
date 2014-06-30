using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ShortStuff.Data;
using ShortStuff.Domain.Entities;

namespace ShortStuff.Repository
{
    public class UserRepository : RepositoryBase<User, string>
    {
        private readonly ShortStuffContext _context;

        public UserRepository(ShortStuffContext context)
        {
            _context = context;
        }

        public override IEnumerable<User> GetAll()
        {
            IEnumerable<User> user = _context.Users.Project()
                           .To<User>("Followers");
            return user;
        }

        public override User GetById(string id)
        {
            return _context.Users.Project()
                           .To<User>()
                           .FirstOrDefault(u => u.Id == id);
        }

        public override string Create(User entity)
        {
            var user = AutoMapper.Mapper.Map<Data.Entities.User>(entity);
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.Id;
        }

        public override void Update(User entity)
        {
            var user = AutoMapper.Mapper.Map<Data.Entities.User>(entity);

            var dbUser = _context.ChangeTracker.Entries<Data.Entities.User>()
                                 .FirstOrDefault(u => u.Entity.Id == user.Id);

            if (dbUser != null)
            {
                dbUser.CurrentValues.SetValues(user);
            }
            else
            {
                var tempUser = new Data.Entities.User
                {
                    Id = user.Id
                };
                _context.Users.Attach(tempUser);
                _context.Entry(tempUser).CurrentValues.SetValues(user);
            }
            _context.SaveChanges();
        }

        public override void Delete(User entity)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == entity.Id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
