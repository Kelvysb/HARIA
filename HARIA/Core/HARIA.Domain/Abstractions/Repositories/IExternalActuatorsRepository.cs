using System.Threading.Tasks;
using HARIA.Domain.Entities;

namespace HARIA.Domain.Abstractions.Repositories
{
    public interface IExternalActuatorsRepository : IRepositoryBase<ExternalActuatorEntity>
    {
        Task<ExternalActuatorEntity> GetBycode(string code);
    }
}