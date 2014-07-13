// ShortStuff.Domain
// IRepository.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Helpers;

namespace ShortStuff.Domain
{
    public interface IRepository<TDomain, TId> where TDomain : EntityBase<TId>
    {
        IEnumerable<TDomain> GetWhere<TSearch>(Expression<Func<TDomain, TSearch>> predicate, TSearch value, ExpressionType type);
        TDomain GetOne<TSearch>(Expression<Func<TDomain, TSearch>> predicate, TSearch value, ExpressionType type);
        Task<IEnumerable<TDomain>> GetWhereAsync<TSearch>(Expression<Func<TDomain, TSearch>> predicate, TSearch value, ExpressionType type);
        Task<TDomain> GetOneAsync<TSearch>(Expression<Func<TDomain, TSearch>> predicate, TSearch value, ExpressionType type);
        IEnumerable<TDomain> GetAll();
        Task<IEnumerable<TDomain>> GetAllAsync();
        TDomain GetById(TId id);
        Task<TDomain> GetByIdAsync(TId id);
        ActionStatus<TId> Create(TDomain entity);
        Task<ActionStatus<TId>> CreateAsync(TDomain entity);
        ActionStatus<TId> Update(TDomain entity);
        Task<ActionStatus<TId>> UpdateAsync(TDomain entity);
        void Delete(TId id);
        Task DeleteAsync(TId id);
    }
}
