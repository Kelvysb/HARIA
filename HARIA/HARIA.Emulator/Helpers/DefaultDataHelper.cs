using System.Collections.Generic;
using HARIA.Domain.DTOs;

namespace HARIA.Emulator.Helpers
{
    public abstract class DefaultDataHelper
    {
        private static I18nText.DefaultData translate;

        public static (List<Ambient>, List<Device>, List<ActionEvent>) GetDefaultData(I18nText.DefaultData translate)
        {
            DefaultDataHelper.translate = translate;
            var ambients = CreateAmbients();
            var devices = CreateDevices();
            var actions = new List<ActionEvent>();
            foreach (var device in devices)
            {
                actions.AddRange(CreateActions(device));
            }
            return (ambients, devices, actions);
        }

        private static List<ActionEvent> CreateActions(Device devices)
        {
            return new List<ActionEvent>()
            {
                new ActionEvent() {
                    Id = 0,
                    Description = $"{translate.Action} {devices.Sensors[0].Code} - {devices.Actuators[0].Code}",
                    Type = Domain.Enums.ActionType.Sensor,
                    ActuatorMessage = "ON",
                    Invert = false,
                    Sensors = devices.Sensors.GetRange(0, 1),
                    Actuators =  devices.Actuators.GetRange(0, 1),
                    Scenarios = new List<Scenario>() { new Scenario() { Id = 1} }
                },
                new ActionEvent() {
                    Id = 0,
                    Description = $"{translate.Action} {devices.Sensors[1].Code} - {devices.Actuators[1].Code}",
                    Type = Domain.Enums.ActionType.Sensor,
                    ActuatorMessage = "ON",
                    Invert = false,
                    Sensors = devices.Sensors.GetRange(1, 1),
                    Actuators =  devices.Actuators.GetRange(1, 1),
                    Scenarios = new List<Scenario>() { new Scenario() { Id = 1} }
                }
            };
        }

        private static List<ScenarioTrigger> CreateTriggers()
        {
            return new List<ScenarioTrigger>()
            {
               new ScenarioTrigger() { Id = 0, Description = translate.TestScenario, Type = Domain.Enums.ScenarioTriggerType.Manual}
            };
        }

        private static List<Ambient> CreateAmbients()
        {
            return new List<Ambient>()
                {
                    new Ambient(){Id = 0, Description = translate.LivingRoom},
                    new Ambient(){Id = 0, Description = translate.BedRoom},
                    new Ambient(){Id = 0, Description = translate.Kitchen}
                };
        }

        private static List<Device> CreateDevices()
        {
            return new List<Device>()
            {
                new Device() {Id = 0, Code = "01", Description = $"{translate.Device} 1", Actuators = CreateDefaultActivators("01", 1), Sensors = CreateDefaultSensors("01", 1)},
                new Device() {Id = 0, Code = "02", Description = $"{translate.Device} 2", Actuators = CreateDefaultActivators("02", 1), Sensors = CreateDefaultSensors("02", 1)},
                new Device() {Id = 0, Code = "03", Description = $"{translate.Device} 3", Actuators = CreateDefaultActivators("03", 2), Sensors = CreateDefaultSensors("03", 2)},
                new Device() {Id = 0, Code = "04", Description = $"{translate.Device} 4", Actuators = CreateDefaultActivators("04", 2), Sensors = CreateDefaultSensors("04", 2)},
                new Device() {Id = 0, Code = "05", Description = $"{translate.Device} 5", Actuators = CreateDefaultActivators("05", 2), Sensors = CreateDefaultSensors("05", 2)},
                new Device() {Id = 0, Code = "06", Description = $"{translate.Device} 6", Actuators = CreateDefaultActivators("06", 3), Sensors = CreateDefaultSensors("06", 3)},
            };
        }

        private static List<Sensor> CreateDefaultSensors(string code, int ambientId)
        {
            return new List<Sensor>()
            {
                new Sensor() {Id = 0, Code = $"{code};S;01", Description = $"{translate.Sensor} - {code};S;01", AmbientId = ambientId},
                new Sensor() {Id = 0, Code = $"{code};S;02", Description = $"{translate.Sensor} - {code};S;02", AmbientId = ambientId}
            };
        }

        private static List<Actuator> CreateDefaultActivators(string code, int ambientId)
        {
            return new List<Actuator>()
            {
                new Actuator() {Id = 0, Code = $"{code};A;01", Description = $"{translate.Actuator} - {code};A;01", DefaultActiveTime = 30, AmbientId = ambientId},
                new Actuator() {Id = 0, Code = $"{code};A;02", Description = $"{translate.Actuator} - {code};A;02", DefaultActiveTime = 30, AmbientId = ambientId}
            };
        }
    }
}