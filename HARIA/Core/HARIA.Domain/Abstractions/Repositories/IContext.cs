using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HARIA.Domain.Abstractions.Repositories
{
    public interface IContext
    {
        public DbSet<T> GetSet<T>() where T : class;

        public Task<int> SaveChangesAsync();

        public void Atach<T>(T entity) where T : class;
    }
}