using HARIA.Domain.Abstractions.Data;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Models;
using HARIA.StateKeeper.Abstractions;
using Serilog;

namespace HARIA.StateKeeper.Services
{
    public class HariaStateKeeperService : IHariaStateKeeperService
    {
        private IMqttService mqttService;
        private IDeviceDataService deviceDataService;

        public HariaStateKeeperService(
            IMqttService mqttService,
            IDeviceDataService deviceDataService)
        {
            this.mqttService = mqttService;
            this.deviceDataService = deviceDataService;
        }

        public async Task InitializeService()
        {
            try
            {
                await mqttService.Subscribe(async deviceData => await DeviceDataUpdateAsync(deviceData));
                await mqttService.StartPublisher();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message);
            }          
        }

        private async Task DeviceDataUpdateAsync(DeviceData deviceData)
        {
            try
            {
                var currentDevice = await deviceDataService.Get(deviceData.DeviceId);

                if (currentDevice != null && currentDevice.Equals(deviceData))
                    return;

                await deviceDataService.Upsert(deviceData);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, ex.Message);
            }        
        }


    }
}
