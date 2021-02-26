using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Constants;
using HARIA.Domain.DTOs;

namespace HARIA.Services
{
    public class EngineService : IEngineService
    {
        private readonly IDevicesService devicesService;
        private readonly ISensorsService sensorsService;
        private readonly IActuatorsService actuatorsService;
        private readonly IScenariosService scenariosService;
        private readonly IScenarioTriggersService scenarioTriggersService;
        private readonly IExternalSensorsService externalSensorsService;
        private readonly IExternalActuatorsService externalActuatorsService;
        private readonly IStatesService statesService;

        public EngineService(
            IDevicesService devicesService,
            ISensorsService sensorsService,
            IActuatorsService actuatorsService,
            IScenariosService scenariosService,
            IScenarioTriggersService scenarioTriggersService,
            IExternalSensorsService externalSensorsService,
            IExternalActuatorsService externalActuatorsService)
        {
            this.devicesService = devicesService;
            this.sensorsService = sensorsService;
            this.actuatorsService = actuatorsService;
            this.scenariosService = scenariosService;
            this.scenarioTriggersService = scenarioTriggersService;
            this.externalSensorsService = externalSensorsService;
            this.externalActuatorsService = externalActuatorsService;
        }

        public async Task<List<DeviceMessage>> StateChange(List<DeviceMessage> deviceMessages)
        {
            List<DeviceMessage> result = new List<DeviceMessage>();
            if (deviceMessages.Any())
            {
                string deviceCode = deviceMessages.FirstOrDefault().DeviceCode;
                await UpdateDevieStatus(deviceCode);
                foreach (DeviceMessage message in deviceMessages)
                {
                    await ProcessSensorMessasge(message);
                }
                await ExecuteActions();
                result.AddRange(await GetState(deviceCode));
            }
            return result;
        }

        public async Task<List<DeviceMessage>> GetState(string deviceCode)
        {
            Device device = await devicesService.GetByCode(deviceCode);
            List<Actuator> actuators = await actuatorsService.GetByDevice(device.Id);
            return await ProcessActuatorMessasge(actuators, deviceCode);
        }

        public async Task ExecuteActions()
        {
            var states = await statesService.GetStateDictionary();
            var scenarios = await scenariosService.Get();
            var scenarioTriggers = await scenarioTriggersService.Get();
            var devicecs = await devicesService.Get();
        }

        private async Task UpdateDevieStatus(string deviceCode)
        {
            Device device = await devicesService.GetByCode(deviceCode);
            device.LastActivity = DateTime.UtcNow;
            await devicesService.Update(device);
        }

        private async Task ProcessSensorMessasge(DeviceMessage deviceMessage)
        {
            if (deviceMessage.Type.Equals(DeviceType.SENSOR, StringComparison.InvariantCultureIgnoreCase))
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

        private async Task<List<DeviceMessage>> ProcessActuatorMessasge(List<Actuator> actuators, string deviceCode)
        {
            List<DeviceMessage> result = new List<DeviceMessage>();

            foreach (var actuator in actuators)
            {
                bool expectedState = actuator.Active;
                if (actuator.DeactivationTime.HasValue)
                {
                    expectedState = actuator.DeactivationTime.Value.CompareTo(DateTime.Now) <= 0;
                }
                if (expectedState != actuator.Active)
                {
                    actuator.Active = expectedState;
                    await actuatorsService.Update(actuator);
                }

                result.Add(
                     new DeviceMessage()
                     {
                         Code = actuator.Code,
                         Value = actuator.Active ? 1 : 0,
                         DeviceCode = deviceCode,
                         Type = DeviceType.ACTUATOR,
                         Message = actuator.Message
                     });
            }
            return result;
        }
    }
}