using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ShortStuff.Domain.Entities;

namespace ShortStuff.Domain
{
    public interface IRepository<T, TId> where T : EntityBase<TId>
    {
        IEnumerable<T> GetWhere(Expression<Func<T, bool>> expression);
        T GetFirstOrDefault(Expression<Func<T, bool>> expression);

        IEnumerable<T> GetAll();
        T GetById(TId id);
        TId Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
