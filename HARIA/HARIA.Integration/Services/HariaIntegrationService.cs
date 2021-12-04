using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Models;
using HARIA.Integration.Abstractions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HARIA.Integration.Services
{
    public class HariaIntegrationService : IHariaIntegrationService
    {
        private readonly IDeviceDataService deviceDataService;
        private readonly IMqttService mqttService;

        public HariaIntegrationService(IDeviceDataService deviceDataService,
                                       IMqttService mqttService)
        {
            this.deviceDataService = deviceDataService;
            this.mqttService = mqttService;
        }

        public async Task<DeviceState?> GetActuator(string deviceId, string itemId)
        {
            var device = await deviceDataService.Get(deviceId);
            if (device == null) return null;
            return GetDeviceStatusFromActuators(itemId, device);
        }

        public async Task<List<DeviceState?>> GetActuators()
        {
            var devices = await deviceDataService.GetAll();
            if (!devices.Any()) return new List<DeviceState?>();
            return devices
                .Where(device => device.Actuators != null)
                .SelectMany(device =>
                new List<DeviceState?>
                {
                    GetDeviceStatusFromActuators("D0", device),
                    GetDeviceStatusFromActuators("D1", device),
                    GetDeviceStatusFromActuators("D2", device),
                    GetDeviceStatusFromActuators("D3", device),
                    GetDeviceStatusFromActuators("D4", device),
                    GetDeviceStatusFromActuators("D5", device),
                    GetDeviceStatusFromActuators("D6", device),
                    GetDeviceStatusFromActuators("D7", device),
                    GetDeviceStatusFromActuators("D8", device)
                }.Where(item => item != null)
            ).ToList();
        }

        public async Task<DeviceState?> GetSensor(string deviceId, string itemId)
        {
            var device = await deviceDataService.Get(deviceId);
            if (device == null) return null;
            return GetDeviceStatusFromSensors(itemId, device);
        }

        public async Task<List<DeviceState?>> GetSensors()
        {
            var devices = await deviceDataService.GetAll();
            if (!devices.Any()) return new List<DeviceState?>();
            return devices
                .Where(device => device.Sensors != null)
                .SelectMany(device =>
                new List<DeviceState?>
                {
                    GetDeviceStatusFromSensors("D0", device),
                    GetDeviceStatusFromSensors("D1", device),
                    GetDeviceStatusFromSensors("D2", device),
                    GetDeviceStatusFromSensors("D3", device),
                    GetDeviceStatusFromSensors("D4", device),
                    GetDeviceStatusFromSensors("D5", device),
                    GetDeviceStatusFromSensors("D6", device),
                    GetDeviceStatusFromSensors("D7", device),
                    GetDeviceStatusFromSensors("D8", device)
                }.Where(item => item != null)
            ).ToList();
        }

        public async Task SetActuator(string deviceId, string itemId, bool value)
        {
            var device = await deviceDataService.Get(deviceId);
            if (device == null) return;
            device = SetModifiedActuator(itemId, device, value);
            await mqttService.StartPublisher();
            var message = JsonSerializer.Serialize(device, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
            await mqttService.SendMessage(message, $"devices/{device.DeviceId}/set");            
        }

        private static DeviceState? GetDeviceStatusFromActuators(string itemId, DeviceData device)
        {
            var item = GetActuatorItem(itemId, device);
            if (item == null) return null;
            return new DeviceState { DeviceId = device.DeviceId, ItemId = itemId, Type = "Actuator", Description = item?.Description, Value = item.Value };
        }

        private static DeviceState? GetDeviceStatusFromSensors(string itemId, DeviceData device)
        {
            var item = GetSensorItem(itemId, device);
            if (item == null) return null;
            return new DeviceState { DeviceId = device.DeviceId, ItemId = itemId, Type = "Sensor", Description = item?.Description, Value = item.Value };
        }

        private static DigitalElement? GetActuatorItem(string itemId, DeviceData device)
        {
            return itemId switch
            {
                "D0" => device.Actuators.D0,
                "D1" => device.Actuators.D1,
                "D2" => device.Actuators.D2,
                "D3" => device.Actuators.D3,
                "D4" => device.Actuators.D4,
                "D5" => device.Actuators.D5,
                "D6" => device.Actuators.D6,
                "D7" => device.Actuators.D7,
                "D8" => device.Actuators.D8,
                _ => null
            };
        }

        private static DigitalElement? GetSensorItem(string itemId, DeviceData device)
        {
            return itemId switch
            {
                "D0" => device.Sensors.D0,
                "D1" => device.Sensors.D1,
                "D2" => device.Sensors.D2,
                "D3" => device.Sensors.D3,
                "D4" => device.Sensors.D4,
                "D5" => device.Sensors.D5,
                "D6" => device.Sensors.D6,
                "D7" => device.Sensors.D7,
                "D8" => device.Sensors.D8,
                _ => null
            };
        }

        private static DeviceData SetModifiedActuator(string itemId, DeviceData device, bool value)
        {         
            switch (itemId)
            {
                case "D0":
                    device.Actuators.D0.Value = value;
                    break;
                case "D1":
                    device.Actuators.D1.Value = value;
                    break;
                case "D2":
                    device.Actuators.D2.Value = value;
                    break;
                case "D3":
                    device.Actuators.D3.Value = value;
                    break;
                case "D4":
                    device.Actuators.D4.Value = value;
                    break;
                case "D5":
                    device.Actuators.D5.Value = value;
                    break;
                case "D6":
                    device.Actuators.D6.Value = value;
                    break;
                case "D7":
                    device.Actuators.D7.Value = value;
                    break;
                case "D8":
                    device.Actuators.D8.Value = value;
                    break;
            };
            return device;
        }
    }
}
