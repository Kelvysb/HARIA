using System.Threading.Tasks;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Domain.Abstractions.Services
{
    public interface IExternalSensorsService : IServiceBase<ExternalSensorEntity, ExternalSensor>
    {
        Task<ExternalSensor> GetByCode(string code);
    }
}