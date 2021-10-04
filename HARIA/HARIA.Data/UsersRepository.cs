using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Data;
using HARIA.Domain.Entities;
using HARIA.Domain.Models;
using MongoDB.Driver;

namespace HARIA.Data
{
    public class UsersRepository : RepositoryBase<UserEntity>, IUsersRepository
    {
        public UsersRepository(MongoDbConfig mongoDbConfig) : base(mongoDbConfig)
        {
        }

        protected override async Task CreateIndexes()
        {
            await collection.Indexes.CreateOneAsync(
                new CreateIndexModel<UserEntity>(
                    Builders<UserEntity>.IndexKeys.Ascending(user => user.Permissions)
                    )
                );
        }
    }
}