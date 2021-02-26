using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Domain.DTOs;

namespace HARIA.Domain.Abstractions.Client
{
    public interface IEngineClient
    {
        Task<List<DeviceMessage>> StateChange(List<DeviceMessage> deviceMessages, string token);

        Task<List<DeviceMessage>> GetState(string deviceCode, string token);
    }
}