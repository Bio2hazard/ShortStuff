using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using Omu.ValueInjecter;
using ShortStuff.Data;
using ShortStuff.Domain.Entities;
using ShortStuff.Repository.ValueInjecter;

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
            return _context.Users.BuildUser();
        }

        public override User GetById(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id).BuildUser();
        }

        public override string Create(User entity)
        {
            var user = new Data.Entities.User();
            user.InjectFrom<SmartConventionInjection>(entity);
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.Id;
        }

        public override void Update(User entity)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.Id == entity.Id).InjectFrom<NotNullInjection>(entity);

            var changeTrackerUser = _context.ChangeTracker.Entries<Data.Entities.User>()
                                            .FirstOrDefault(u => u.Entity.Id == entity.Id);

            changeTrackerUser.CurrentValues.SetValues(dbUser);
            _context.SaveChanges();

            //if (dbUser != null)
            //{
            //    dbUser.CurrentValues.SetValues(user);
            //}
            //else
            //{
            //    var tempUser = new Data.Entities.User
            //    {
            //        Id = user.Id
            //    };
            //    _context.Users.Attach(tempUser);
            //    _context.Entry(tempUser).CurrentValues.SetValues(user);
            //}
            //_context.SaveChanges();
        }

        public override void Delete(User entity)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == entity.Id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
