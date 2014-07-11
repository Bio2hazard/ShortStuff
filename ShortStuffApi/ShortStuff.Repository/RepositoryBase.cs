// ShortStuff.Repository
// RepositoryBase.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Omu.ValueInjecter;
using ShortStuff.Data;
using ShortStuff.Data.Entities;
using ShortStuff.Domain;
using ShortStuff.Domain.Entities;
using ShortStuff.Domain.Enums;
using ShortStuff.Domain.Helpers;
using ShortStuff.Repository.Extensions;
using ShortStuff.Repository.ValueInjecter;

namespace ShortStuff.Repository
{
    public abstract class RepositoryBase<TData, TDomain, TId> : IRepository<TDomain, TId> where TDomain : EntityBase<TId>, new() where TData : class, IDataEntity<TId>, new()
    {
        protected readonly ShortStuffContext Context;

        public RepositoryBase(ShortStuffContext context)
        {
            Context = context;
        }

        public IEnumerable<TDomain> GetWhere<TSearch>(Expression<Func<TDomain, TSearch>> predicate, TSearch value, ExpressionType type)
        {
            var convertedPredicate = predicate.ConvertExpression<TData, TDomain, TSearch>();
            return Context.Set<TData>()
                           .Lambda<TData, TSearch>(convertedPredicate, value, type)
                           .BuildQuery<TData, TDomain>();
        }

        public TDomain GetOne<TSearch>(Expression<Func<TDomain, TSearch>> predicate, TSearch value, ExpressionType type)
        {
            var convertedPredicate = predicate.ConvertExpression<TData, TDomain, TSearch>();
            return Context.Set<TData>()
                           .Lambda(convertedPredicate, value, type)
                           .FirstOrDefault()
                           .BuildEntity(new TDomain());
        }

        public async Task<IEnumerable<TDomain>> GetWhereAsync<TSearch>(Expression<Func<TDomain, TSearch>> predicate, TSearch value, ExpressionType type)
        {
            var convertedPredicate = predicate.ConvertExpression<TData, TDomain, TSearch>();
            return await Context.Set<TData>()
                                 .Lambda(convertedPredicate, value, type)
                                 .BuildQueryAsync<TData, TDomain>();
        }

        public async Task<TDomain> GetOneAsync<TSearch>(Expression<Func<TDomain, TSearch>> predicate, TSearch value, ExpressionType type)
        {
            var convertedPredicate = predicate.ConvertExpression<TData, TDomain, TSearch>();
            var result = await Context.Set<TData>()
                                       .Lambda(convertedPredicate, value, type)
                                       .FirstOrDefaultAsync();

            return result.BuildEntity(new TDomain());
        }

        public IEnumerable<TDomain> GetAll()
        {
            return Context.Set<TData>()
                           .BuildQuery<TData, TDomain>();
        }

        public async Task<IEnumerable<TDomain>> GetAllAsync()
        {
            return await Context.Set<TData>()
                                 .BuildQueryAsync<TData, TDomain>();
        }

        public TDomain GetById(TId id)
        {
            return Context.Set<TData>()
                           .Find(id)
                           .BuildEntity(new TDomain());
        }

        public async Task<TDomain> GetByIdAsync(TId id)
        {
            var result = await Context.Set<TData>()
                                       .FindAsync(id);

            return result.BuildEntity(new TDomain());
        }

        public CreateStatus<TId> Create(TDomain domainEntity)
        {
            if (Context.Set<TData>()
                        .Lambda(u => u.Id, domainEntity.Id)
                        .Any())
            {
                return new CreateStatus<TId>
                {
                    Status = CreateStatusEnum.Conflict
                };
            }

            var dataEntity = new TData();
            dataEntity.InjectFrom<SmartConventionInjection>(domainEntity);
            Context.Set<TData>()
                    .Add(dataEntity);
            Context.SaveChanges();

            return new CreateStatus<TId>
            {
                Status = CreateStatusEnum.Created,
                Id = dataEntity.Id
            };
        }

        public async Task<CreateStatus<TId>> CreateAsync(TDomain domainEntity)
        {
            if (await Context.Set<TData>()
                              .Lambda(u => u.Id, domainEntity.Id)
                              .AnyAsync())
            {
                return new CreateStatus<TId>
                {
                    Status = CreateStatusEnum.Conflict
                };
            }

            var dataEntity = new TData();
            dataEntity.InjectFrom<SmartConventionInjection>(domainEntity);
            Context.Set<TData>()
                    .Add(dataEntity);
            await Context.SaveChangesAsync();

            return new CreateStatus<TId>
            {
                Status = CreateStatusEnum.Created,
                Id = dataEntity.Id
            };
        }

        public UpdateStatus Update(TDomain domainEntity)
        {
            var dataEntity = Context.Set<TData>()
                                     .Lambda(u => u.Id, domainEntity.Id)
                                     .FirstOrDefault();

            if (dataEntity == null)
            {
                return UpdateStatus.NotFound;
            }

            var updatedDataEntity = dataEntity.InjectFrom<NotNullInjection>(domainEntity);

            var changeTracker = Context.ChangeTracker.Entries<TData>()
                                        .FirstOrDefault(u => Equals(u.Entity.Id, domainEntity.Id));

            // ReSharper disable once PossibleNullReferenceException
            changeTracker.CurrentValues.SetValues(updatedDataEntity);

            return Context.SaveChanges() == 0 ? UpdateStatus.NoChange : UpdateStatus.Updated;
        }

        public async Task<UpdateStatus> UpdateAsync(TDomain domainEntity)
        {
            var dataEntity = await Context.Set<TData>()
                                           .Lambda(u => u.Id, domainEntity.Id)
                                           .FirstOrDefaultAsync();

            if (dataEntity == null)
            {
                return UpdateStatus.NotFound;
            }

            var updatedDataEntity = dataEntity.InjectFrom<NotNullInjection>(domainEntity);

            var changeTracker = Context.ChangeTracker.Entries<TData>()
                                        .FirstOrDefault(u => Equals(u.Entity.Id, domainEntity.Id));

            // ReSharper disable once PossibleNullReferenceException
            changeTracker.CurrentValues.SetValues(updatedDataEntity);

            return await Context.SaveChangesAsync() == 0 ? UpdateStatus.NoChange : UpdateStatus.Updated;
        }

        public void Delete(TId id)
        {
            var dataEntity = Context.Set<TData>()
                                     .Lambda(u => u.Id, id)
                                     .FirstOrDefault();
            Context.Set<TData>()
                    .Remove(dataEntity);
            Context.SaveChanges();
        }

        public async Task DeleteAsync(TId id)
        {
            var dataEntity = await Context.Set<TData>()
                                           .Lambda(u => u.Id, id)
                                           .FirstOrDefaultAsync();
            Context.Set<TData>()
                    .Remove(dataEntity);
            await Context.SaveChangesAsync();
        }
    }
}
