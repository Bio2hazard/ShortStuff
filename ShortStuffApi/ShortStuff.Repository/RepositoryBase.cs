// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryBase.cs" company="Bio2hazard">
//   Licensed under GNU GPL v2.0.
//   See License/GPLv2.txt for details.
// </copyright>
// <summary>
//   The repository base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ShortStuff.Repository
{
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

    /// <summary>
    /// The repository base.
    /// </summary>
    /// <typeparam name="TData">
    /// </typeparam>
    /// <typeparam name="TDomain">
    /// </typeparam>
    /// <typeparam name="TId">
    /// </typeparam>
    public abstract class RepositoryBase<TData, TDomain, TId> : IRepository<TDomain, TId>
        where TDomain : EntityBase<TId>, new() where TData : class, IDataEntity<TId>, new()
    {
        /// <summary>
        /// The context.
        /// </summary>
        protected readonly ShortStuffContext Context;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{TData,TDomain,TId}"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public RepositoryBase(ShortStuffContext context)
        {
            Context = context;
        }

        /// <summary>
        /// The get where.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <typeparam name="TSearch">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<TDomain> GetWhere<TSearch>(Expression<Func<TDomain, TSearch>> predicate, TSearch value, ExpressionType type)
        {
            var convertedPredicate = predicate.ConvertExpression<TData, TDomain, TSearch>();
            return Context.Set<TData>().Lambda<TData, TSearch>(convertedPredicate, value, type).BuildQuery<TData, TDomain>();
        }

        /// <summary>
        /// The get one.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <typeparam name="TSearch">
        /// </typeparam>
        /// <returns>
        /// The <see cref="TDomain"/>.
        /// </returns>
        public TDomain GetOne<TSearch>(Expression<Func<TDomain, TSearch>> predicate, TSearch value, ExpressionType type)
        {
            var convertedPredicate = predicate.ConvertExpression<TData, TDomain, TSearch>();
            return Context.Set<TData>().Lambda(convertedPredicate, value, type).FirstOrDefault().BuildEntity(new TDomain());
        }

        /// <summary>
        /// The get where async.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <typeparam name="TSearch">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IEnumerable<TDomain>> GetWhereAsync<TSearch>(Expression<Func<TDomain, TSearch>> predicate, TSearch value, ExpressionType type)
        {
            var convertedPredicate = predicate.ConvertExpression<TData, TDomain, TSearch>();
            return await Context.Set<TData>().Lambda(convertedPredicate, value, type).BuildQueryAsync<TData, TDomain>();
        }

        /// <summary>
        /// The get one async.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <typeparam name="TSearch">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<TDomain> GetOneAsync<TSearch>(Expression<Func<TDomain, TSearch>> predicate, TSearch value, ExpressionType type)
        {
            var convertedPredicate = predicate.ConvertExpression<TData, TDomain, TSearch>();
            var result = await Context.Set<TData>().Lambda(convertedPredicate, value, type).FirstOrDefaultAsync();

            return result.BuildEntity(new TDomain());
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<TDomain> GetAll()
        {
            return Context.Set<TData>().BuildQuery<TData, TDomain>();
        }

        /// <summary>
        /// The get all async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IEnumerable<TDomain>> GetAllAsync()
        {
            return await Context.Set<TData>().BuildQueryAsync<TData, TDomain>();
        }

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="TDomain"/>.
        /// </returns>
        public TDomain GetById(TId id)
        {
            return Context.Set<TData>().Find(id).BuildEntity(new TDomain());
        }

        /// <summary>
        /// The get by id async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<TDomain> GetByIdAsync(TId id)
        {
            var result = await Context.Set<TData>().FindAsync(id);

            return result.BuildEntity(new TDomain());
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="domainEntity">
        /// The domain entity.
        /// </param>
        /// <returns>
        /// The <see cref="ActionStatus"/>.
        /// </returns>
        public ActionStatus<TId> Create(TDomain domainEntity)
        {
            if (Context.Set<TData>().Lambda(u => u.Id, domainEntity.Id).Any())
            {
                return new ActionStatus<TId> { Status = ActionStatusEnum.Conflict };
            }

            var dataEntity = new TData();
            dataEntity.InjectFrom<SmartConventionInjection>(domainEntity);
            Context.Set<TData>().Add(dataEntity);
            Context.SaveChanges();

            return new ActionStatus<TId> { Status = ActionStatusEnum.Success, SubStatus = ActionSubStatusEnum.Created, Id = dataEntity.Id };
        }

        /// <summary>
        /// The create async.
        /// </summary>
        /// <param name="domainEntity">
        /// The domain entity.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<ActionStatus<TId>> CreateAsync(TDomain domainEntity)
        {
            if (await Context.Set<TData>().Lambda(u => u.Id, domainEntity.Id).AnyAsync())
            {
                return new ActionStatus<TId> { Status = ActionStatusEnum.Conflict };
            }

            var dataEntity = new TData();
            dataEntity.InjectFrom<SmartConventionInjection>(domainEntity);
            Context.Set<TData>().Add(dataEntity);
            await Context.SaveChangesAsync();

            return new ActionStatus<TId> { Status = ActionStatusEnum.Success, SubStatus = ActionSubStatusEnum.Created, Id = dataEntity.Id };
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="domainEntity">
        /// The domain entity.
        /// </param>
        /// <returns>
        /// The <see cref="ActionStatus"/>.
        /// </returns>
        public ActionStatus<TId> Update(TDomain domainEntity)
        {
            var dataEntity = Context.Set<TData>().Lambda(u => u.Id, domainEntity.Id).FirstOrDefault();

            var returnStatus = new ActionStatus<TId>();

            if (dataEntity == null)
            {
                returnStatus.Status = ActionStatusEnum.NotFound;
                return returnStatus;
            }

            var updatedDataEntity = dataEntity.InjectFrom<NotNullInjection>(domainEntity);

            var changeTracker = Context.ChangeTracker.Entries<TData>().FirstOrDefault(u => Equals(u.Entity.Id, domainEntity.Id));

            // ReSharper disable once PossibleNullReferenceException
            changeTracker.CurrentValues.SetValues(updatedDataEntity);

            returnStatus.SubStatus = Context.SaveChanges() == 0 ? ActionSubStatusEnum.NoChange : ActionSubStatusEnum.Updated;

            return returnStatus;
        }

        /// <summary>
        /// The update async.
        /// </summary>
        /// <param name="domainEntity">
        /// The domain entity.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<ActionStatus<TId>> UpdateAsync(TDomain domainEntity)
        {
            var dataEntity = await Context.Set<TData>().Lambda(u => u.Id, domainEntity.Id).FirstOrDefaultAsync();

            var returnStatus = new ActionStatus<TId>();

            if (dataEntity == null)
            {
                returnStatus.Status = ActionStatusEnum.NotFound;
                return returnStatus;
            }

            var updatedDataEntity = dataEntity.InjectFrom<NotNullInjection>(domainEntity);

            var changeTracker = Context.ChangeTracker.Entries<TData>().FirstOrDefault(u => Equals(u.Entity.Id, domainEntity.Id));

            // ReSharper disable once PossibleNullReferenceException
            changeTracker.CurrentValues.SetValues(updatedDataEntity);

            returnStatus.SubStatus = await Context.SaveChangesAsync() == 0 ? ActionSubStatusEnum.NoChange : ActionSubStatusEnum.Updated;
            return returnStatus;
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        public void Delete(TId id)
        {
            var dataEntity = Context.Set<TData>().Lambda(u => u.Id, id).FirstOrDefault();
            Context.Set<TData>().Remove(dataEntity);
            Context.SaveChanges();
        }

        /// <summary>
        /// The delete async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task DeleteAsync(TId id)
        {
            var dataEntity = await Context.Set<TData>().Lambda(u => u.Id, id).FirstOrDefaultAsync();
            Context.Set<TData>().Remove(dataEntity);
            await Context.SaveChangesAsync();
        }
    }
}