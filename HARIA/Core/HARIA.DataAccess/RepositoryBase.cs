using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Entities;
using HARIA.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HARIA.DataAccess
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IEntity, new()
    {
        protected DbSet<TEntity> dbSet;

        protected IContext context;

        public RepositoryBase(IContext context)
        {
            this.context = context;
            dbSet = context.GetSet<TEntity>();
        }

        public virtual Task<int> Add(TEntity entity)
        {
            dbSet.Add(entity);
            return context.SaveChangesAsync();
        }

        public virtual Task<int> Delete(int id)
        {
            dbSet.Remove(new TEntity() { Id = id });
            return context.SaveChangesAsync();
        }

        public virtual Task<List<TEntity>> GetAll()
        {
            return dbSet
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual Task<TEntity> GetById(int id)
        {
            return dbSet
                .Where(t => t.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public virtual Task<int> Update(TEntity entity)
        {
            dbSet.Update(entity);
            return context.SaveChangesAsync();
        }
    }
}