using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Enums;

namespace ShortStuff.Domain
{
    public interface IRepository<T, TId> where T : EntityBase<TId>
    {
        IEnumerable<T> GetWhere(Expression<Func<T, bool>> expression);
        T GetFirstOrDefault(Expression<Func<T, bool>> expression);

        IEnumerable<T> GetAll();
        T GetById(TId id);
        CreateStatus<TId> Create(T entity);
        UpdateStatus Update(T entity);
        void Delete(TId id);
    }
}
