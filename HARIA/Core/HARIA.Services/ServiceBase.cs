using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HARIA.Domain.Abstractions.DTOs;
using HARIA.Domain.Abstractions.Entities;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;

namespace HARIA.Services
{
    public abstract class ServiceBase<TEntity, TDTO> : IServiceBase<TEntity, TDTO>
        where TEntity : class, IEntity, new()
        where TDTO : class, IDTO, new()
    {
        protected IRepositoryBase<TEntity> repository;
        protected IMapper mapper;

        protected ServiceBase(IRepositoryBase<TEntity> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public virtual Task<int> Add(TDTO dto)
        {
            TEntity entity = mapper.Map<TEntity>(dto);
            return repository.Add(entity);
        }

        public virtual Task<int> Delete(int id)
        {
            return repository.Delete(id);
        }

        public virtual async Task<List<TDTO>> Get()
        {
            List<TEntity> entities = await repository.GetAll();
            return mapper.Map<List<TDTO>>(entities);
        }

        public virtual async Task<TDTO> Get(int id)
        {
            IEntity entity = await repository.GetById(id);
            return mapper.Map<TDTO>(entity);
        }

        public virtual Task<int> Update(TDTO dto)
        {
            TEntity entity = mapper.Map<TEntity>(dto);
            return repository.Update(entity);
        }
    }
}