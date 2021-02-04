using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Constants;
using HARIA.Domain.DTOs;

namespace HARIA.Services
{
    public class MessagesService
    {
        private readonly IDevicesService devicesService;
        private readonly ISensorsService sensorsService;
        private readonly IActuatorsService actuatorsService;
        private readonly IEngineService engineService;

        public MessagesService(
            IDevicesService devicesService,
            ISensorsService sensorsService,
            IActuatorsService actuatorsService,
            IEngineService engineService)
        {
            this.devicesService = devicesService;
            this.sensorsService = sensorsService;
            this.actuatorsService = actuatorsService;
            this.engineService = engineService;
        }

        public async Task<List<DeviceMessage>> ProcessMessages(List<DeviceMessage> deviceMessages)
        {
            List<DeviceMessage> result = new List<DeviceMessage>();
            if (deviceMessages.Any())
            {
                string deviceCode = deviceMessages.FirstOrDefault().DeviceCode;
                await UpdateDevieStatus(deviceCode);
                foreach (DeviceMessage message in deviceMessages)
                {
                    await ProcessMesasge(message);
                }
                await engineService.Execute();
                result.AddRange(await RetrieveResults(deviceCode));
            }
            return result;
        }

        public async Task<List<DeviceMessage>> Snapshot(string deviceCode)
        {
            List<DeviceMessage> result = new List<DeviceMessage>();
            Device device = await devicesService.GetByCode(deviceCode);
            List<Sensor> sensors = await sensorsService.GetByDevice(device.Id);
            List<Actuator> actuators = await actuatorsService.GetByDevice(device.Id);
            result.AddRange(sensors.Select(s => new DeviceMessage()
            {
                Code = s.Code,
                Value = s.Value,
                DeviceCode = device.Code,
                Type = Constants.SENSOR_TYPE
            }));
            result.AddRange(actuators.Select(s => new DeviceMessage()
            {
                Code = s.Code,
                Value = s.Active ? 1 : 0,
                DeviceCode = device.Code,
                Type = Constants.ACTUATOR_TYPE
            }));
            return result;
        }

        private async Task UpdateDevieStatus(string deviceCode)
        {
            Device device = await devicesService.GetByCode(deviceCode);
            device.LastActivity = DateTime.UtcNow;
            await devicesService.Update(device);
        }

        private async Task ProcessMesasge(DeviceMessage deviceMessage)
        {
            if (deviceMessage.Type.Equals(Constants.SENSOR_TYPE, StringComparison.InvariantCultureIgnoreCase))
            {
                Sensor sensor = await sensorsService.GetByCode(deviceMessage.Code);
                if (sensor.Value != deviceMessage.Value)
                {
                    sensor.Value = deviceMessage.Value;
                    sensor.Active = (sensor.Value >= sensor.ActiveLowerBound && sensor.Value <= sensor.ActiveUpperBound);
                    sensor.LastStateChange = DateTime.UtcNow;
                    await sensorsService.Update(sensor);
                }
            }
        }

        private async Task<List<DeviceMessage>> RetrieveResults(string deviceCode)
        {
            throw new NotImplementedException();
        }
    }
}