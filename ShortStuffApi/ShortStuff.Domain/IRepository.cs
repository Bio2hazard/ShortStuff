using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Enums;
using ShortStuff.Domain.Helpers;

namespace ShortStuff.Domain
{
    public interface IRepository<TDomain, TId> where TDomain : EntityBase<TId>
    {
        IEnumerable<TDomain> GetAll();
        Task<IEnumerable<TDomain>> GetAllAsync();
        TDomain GetById(TId id);
        Task<TDomain> GetByIdAsync(TId id);
        CreateStatus<TId> Create(TDomain entity);
        Task<CreateStatus<TId>> CreateAsync(TDomain entity);
        UpdateStatus Update(TDomain entity);
        Task<UpdateStatus> UpdateAsync(TDomain entity);
        void Delete(TId id);
        Task DeleteAsync(TId id);
    }
}
