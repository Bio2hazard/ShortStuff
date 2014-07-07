using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
        private readonly ShortStuffContext _context;

        public RepositoryBase(ShortStuffContext context)
        {
            _context = context;
        }

        public IEnumerable<TDomain> GetAll()
        {
            return _context.Set<TData>().BuildQuery<TData, TDomain>();
        }

        public async Task<IEnumerable<TDomain>> GetAllAsync()
        {
            return await _context.Set<TData>().BuildQueryAsync<TData, TDomain>();
        }
        
        public TDomain GetById(TId id)
        {
            return _context.Set<TData>().BuildQuery<TData, TDomain>(x => Equals(x.Id, id));
        }

        public async Task<TDomain> GetByIdAsync(TId id)
        {
            return await _context.Set<TData>().BuildQueryAsync<TData, TDomain>(x => Equals(x.Id, id));
        }

        public CreateStatus<TId> Create(TDomain domainEntity)
        {
            if (_context.Set<TData>().Any(u => Equals(u.Id, domainEntity.Id)))
            {
                return new CreateStatus<TId> { Status = CreateStatusEnum.Conflict };
            }

            var dataEntity = new TData();
            dataEntity.InjectFrom<SmartConventionInjection>(domainEntity);
            _context.Set<TData>().Add(dataEntity);
            _context.SaveChanges();

            return new CreateStatus<TId> { Status = CreateStatusEnum.Created, Id = dataEntity.Id };
        }

        public async Task<CreateStatus<TId>> CreateAsync(TDomain domainEntity)
        {
            if (await _context.Set<TData>().AnyAsync(u => Equals(u.Id, domainEntity.Id)))
            {
                return new CreateStatus<TId> { Status = CreateStatusEnum.Conflict };
            }

            var dataEntity = new TData();
            dataEntity.InjectFrom<SmartConventionInjection>(domainEntity);
            _context.Set<TData>().Add(dataEntity);
            await _context.SaveChangesAsync();

            return new CreateStatus<TId> { Status = CreateStatusEnum.Created, Id = dataEntity.Id };
        }

        public UpdateStatus Update(TDomain domainEntity)
        {
            var dataEntity = _context.Set<TData>().FirstOrDefault(u => Equals(u.Id, domainEntity.Id));

            if (dataEntity == null)
            {
                return UpdateStatus.NotFound;
            }

            var updatedDataEntity = dataEntity.InjectFrom<NotNullInjection>(domainEntity);

            var changeTracker = _context.ChangeTracker.Entries<TData>().FirstOrDefault(u => Equals(u.Entity.Id, domainEntity.Id));

            // ReSharper disable once PossibleNullReferenceException
            changeTracker.CurrentValues.SetValues(updatedDataEntity);

            return _context.SaveChanges() == 0 ? UpdateStatus.NoChange : UpdateStatus.Updated;
        }

        public async Task<UpdateStatus> UpdateAsync(TDomain domainEntity)
        {
            var dataEntity = await _context.Set<TData>().FirstOrDefaultAsync(u => Equals(u.Id, domainEntity.Id));

            if (dataEntity == null)
            {
                return UpdateStatus.NotFound;
            }

            var updatedDataEntity = dataEntity.InjectFrom<NotNullInjection>(domainEntity);

            var changeTracker = _context.ChangeTracker.Entries<TData>().FirstOrDefault(u => Equals(u.Entity.Id, domainEntity.Id));

            // ReSharper disable once PossibleNullReferenceException
            changeTracker.CurrentValues.SetValues(updatedDataEntity);

            return await _context.SaveChangesAsync() == 0 ? UpdateStatus.NoChange : UpdateStatus.Updated;
        }

        public void Delete(TId id)
        {
            var dataEntity = _context.Set<TData>().FirstOrDefault(u => Equals(u.Id, id));
            _context.Set<TData>().Remove(dataEntity);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(TId id)
        {
            var dataEntity = await _context.Set<TData>().FirstOrDefaultAsync(u => Equals(u.Id, id));
            _context.Set<TData>().Remove(dataEntity);
            await _context.SaveChangesAsync();
        }

    //    public abstract IEnumerable<TDomain> GetAll();
    //    public abstract Task<IEnumerable<TDomain>> GetAllAsync();
    //    public abstract TDomain GetById(TId id);
    //    public abstract Task<TDomain> GetByIdAsync(TId id);
    //    public abstract CreateStatus<TId> Create(TDomain domainEntity);
    //    public abstract Task<CreateStatus<TId>> CreateAsync(TDomain domainEntity);
    //    public abstract UpdateStatus Update(TDomain domainEntity);
    //    public abstract Task<UpdateStatus> UpdateAsync(TDomain domainEntity);
    //    public abstract void Delete(TId id);
    //    public abstract Task DeleteAsync(TId id);
    }
}
