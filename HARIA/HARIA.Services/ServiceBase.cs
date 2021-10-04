using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HARIA.Domain.Abstractions.Data;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public abstract class ServiceBase<TEntity, TModel> : IServiceBase<TModel>
          where TEntity : EntityBase, new()
          where TModel : class, new()
    {
        protected IRepositoryBase<TEntity> repository;
        protected IMapper mapper;

        protected ServiceBase(
            IRepositoryBase<TEntity> repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public virtual Task Upsert(TModel dto)
        {
            TEntity entity = mapper.Map<TEntity>(dto);
            return repository.Upsert(entity);
        }

        public virtual Task Delete(string id)
        {
            return repository.Delete(id);
        }

        public virtual async Task<List<TModel>> GetAll()
        {
            List<TEntity> entities = await repository.GetAll();
            return mapper.Map<List<TModel>>(entities);
        }

        public virtual async Task<TModel> Get(string id)
        {
            var entity = await repository.GetById(id);
            return mapper.Map<TModel>(entity);
        }
    }
}