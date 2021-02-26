using HARIA.Domain.Abstractions.Client;
using HARIA.Domain.Configuration;
using HARIA.Domain.DTOs;

namespace HARIA.Client
{
    public class DevicesClient : ClientBase<Device>, IDevicesClient
    {
        public DevicesClient(HariaApiConfig hariaApiConfig) : base(hariaApiConfig, "Devices")
        {
        }
    }
}