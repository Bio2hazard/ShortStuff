using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ShortStuff.Domain;
using ShortStuff.Domain.Entities;

namespace ShortStuff.Repository
{
    public abstract class RepositoryBase<T, TId> : IRepository<T, TId> where T : EntityBase<TId>
    {
        public IEnumerable<T> GetWhere(Expression<Func<T, bool>> expression)
        {
            return GetAll()
                .AsQueryable()
                .Where(expression);
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return GetWhere(expression)
                .FirstOrDefault();
        }

        public abstract IEnumerable<T> GetAll();
        public abstract T GetById(TId id);
        public abstract TId Create(T entity);
        public abstract void Update(T entity);
        public abstract void Delete(T entity);
    }
}
