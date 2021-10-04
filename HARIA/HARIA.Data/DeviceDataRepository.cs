using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Data;
using HARIA.Domain.Entities;
using HARIA.Domain.Models;
using MongoDB.Driver;

namespace HARIA.Data
{
    public class DeviceDataRepository : RepositoryBase<DeviceDataEntity>, IDeviceDataRepository
    {
        public DeviceDataRepository(MongoDbConfig mongoDbConfig) : base(mongoDbConfig)
        {
        }

        protected override async Task CreateIndexes()
        {
            await collection.Indexes.CreateOneAsync(
               new CreateIndexModel<DeviceDataEntity>(
                   Builders<DeviceDataEntity>.IndexKeys.Ascending(device => device.DeviceName)
                   )
               );
        }
    }
}