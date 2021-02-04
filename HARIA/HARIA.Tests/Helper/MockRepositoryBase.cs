using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Entities;
using HARIA.Domain.Abstractions.Repositories;

namespace HARIA.Tests.Helper
{
    public class MockRepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : IEntity
    {
        protected List<TEntity> list = new List<TEntity>();

        public Task<int> Add(TEntity entity)
        {
            return Task.Run(() =>
            {
                entity.Id = list.Max(t => t.Id) + 1;
                list.Add(entity);
                return entity.Id;
            });
        }

        public Task<int> Delete(int id)
        {
            return Task.Run(() =>
            {
                list.RemoveAll(t => t.Id == id);
                return 1;
            });
        }

        public Task<List<TEntity>> GetAll()
        {
            return Task.Run(() =>
            {
                return list;
            });
        }

        public Task<TEntity> GetById(int id)
        {
            return Task.Run(() =>
            {
                return list
                .Where(t => t.Id == id)
                .FirstOrDefault();
            });
        }

        public async Task<int> Update(TEntity entity)
        {
            TEntity current = await GetById(entity.Id);
            if (current == null) return 0;
            await Delete(current.Id);
            await Add(entity);
            return 1;
        }
    }
}