using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Domain.DTOs;

namespace HARIA.Domain.Abstractions.Services
{
    public interface IEngineService
    {
        Task<List<DeviceMessage>> StateChange(List<DeviceMessage> deviceMessages);

        Task<List<DeviceMessage>> GetState(string deviceCode);

        Task ExecuteActions();
    }
}