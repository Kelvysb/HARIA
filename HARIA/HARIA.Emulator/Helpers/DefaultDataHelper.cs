using System.Collections.Generic;
using HARIA.Domain.DTOs;

namespace HARIA.Emulator.Helpers
{
    public abstract class DefaultDataHelper
    {
        private static I18nText.DefaultData translate;

        public static (List<Ambient>, List<Node>, List<ActionEvent>) GetDefaultData(I18nText.DefaultData translate)
        {
            DefaultDataHelper.translate = translate;
            var ambients = CreateAmbients();
            var devices = CreateNodes();
            var actions = new List<ActionEvent>();
            foreach (var device in devices)
            {
                actions.AddRange(CreateActions(device));
            }
            return (ambients, devices, actions);
        }

        private static List<ActionEvent> CreateActions(Node nodes)
        {
            return new List<ActionEvent>()
            {
                new ActionEvent() {
                    Id = 0,
                    Description = $"{translate.Action} {nodes.Sensors[0].Code} - {nodes.Actuators[0].Code}",
                    Type = Domain.Enums.ActionType.Sensor,
                    ActuatorMessage = "ON",
                    Invert = false,
                    Sensors = nodes.Sensors.GetRange(0, 1),
                    Actuators =  nodes.Actuators.GetRange(0, 1),
                    Scenarios = new List<Scenario>() { new Scenario() { Id = 1} }
                },
                new ActionEvent() {
                    Id = 0,
                    Description = $"{translate.Action} {nodes.Sensors[1].Code} - {nodes.Actuators[1].Code}",
                    Type = Domain.Enums.ActionType.Sensor,
                    ActuatorMessage = "ON",
                    Invert = false,
                    Sensors = nodes.Sensors.GetRange(1, 1),
                    Actuators =  nodes.Actuators.GetRange(1, 1),
                    Scenarios = new List<Scenario>() { new Scenario() { Id = 1} }
                }
            };
        }

        private static List<Ambient> CreateAmbients()
        {
            return new List<Ambient>()
                {
                    new Ambient(){Id = 1, Description = translate.LivingRoom},
                    new Ambient(){Id = 2, Description = translate.BedRoom},
                    new Ambient(){Id = 3, Description = translate.Kitchen}
                };
        }

        private static List<Node> CreateNodes()
        {
            return new List<Node>()
            {
                new Node() {Id = 0, Code = "01", Description = $"{translate.Node} 1", Actuators = CreateDefaultActuators("01", 1, 1, 2), Sensors = CreateDefaultSensors("01", 1, 1, 2)},
                new Node() {Id = 0, Code = "02", Description = $"{translate.Node} 2", Actuators = CreateDefaultActuators("02", 1, 3, 4), Sensors = CreateDefaultSensors("02", 1, 3, 4)},
                new Node() {Id = 0, Code = "03", Description = $"{translate.Node} 3", Actuators = CreateDefaultActuators("03", 2, 5, 6), Sensors = CreateDefaultSensors("03", 2, 5, 6)},
                new Node() {Id = 0, Code = "04", Description = $"{translate.Node} 4", Actuators = CreateDefaultActuators("04", 2, 7, 8), Sensors = CreateDefaultSensors("04", 2, 7, 8)},
                new Node() {Id = 0, Code = "05", Description = $"{translate.Node} 5", Actuators = CreateDefaultActuators("05", 2, 9, 10), Sensors = CreateDefaultSensors("05", 2, 9, 10)},
                new Node() {Id = 0, Code = "06", Description = $"{translate.Node} 6", Actuators = CreateDefaultActuators("06", 3, 11, 12), Sensors = CreateDefaultSensors("06", 3, 11, 12)},
            };
        }

        private static List<Sensor> CreateDefaultSensors(string code, int ambientId, params int[] ids)
        {
            var result = new List<Sensor>();
            var local = 1;

            foreach (var id in ids)
            {
                result.Add(new Sensor() { Id = id, Code = $"{code};S;{local}", Description = $"{translate.Sensor} - {code};S;{local}", AmbientId = ambientId, ActiveLowerBound = 1, ActiveUpperBound = 255 });
                local++;
            }

            return result;
        }

        private static List<Actuator> CreateDefaultActuators(string code, int ambientId, params int[] ids)
        {
            var result = new List<Actuator>();
            var local = 1;

            foreach (var id in ids)
            {
                result.Add(new Actuator() { Id = id, Code = $"{code};A;{local}", Description = $"{translate.Actuator} - {code};A;{local}", DefaultActiveTime = 30, Active = false, AmbientId = ambientId });
                local++;
            }

            return result;
        }
    }
}