using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ShortStuff.Domain;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Enums;

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
        public abstract CreateStatus<TId> Create(T entity);
        public abstract UpdateStatus Update(T entity);
        public abstract void Delete(TId id);
    }
}
