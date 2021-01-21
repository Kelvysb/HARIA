using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Entities;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;

namespace HARIA.Core.Services
{
    public abstract class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class, IEntity, new()
    {
        protected IRepositoryBase<TEntity> repository;

        protected ServiceBase(IRepositoryBase<TEntity> repository)
        {
            this.repository = repository;
        }

        public virtual Task<int> Add(TEntity entity)
        {
            return repository.Add(entity);
        }

        public virtual Task<int> Delete(int id)
        {
            return repository.Delete(id);
        }

        public virtual Task<List<TEntity>> Get()
        {
            return repository.GetAll();
        }

        public virtual Task<TEntity> Get(int id)
        {
            return repository.GetById(id);
        }

        public virtual Task<int> Update(TEntity entity)
        {
            return repository.Update(entity);
        }
    }
}