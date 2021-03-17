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
        private readonly INodesService nodesService;
        private readonly ISensorsService sensorsService;
        private readonly IActuatorsService actuatorsService;
        private readonly IScenariosService scenariosService;
        private readonly IExternalActuatorsService externalActuatorsService;
        private readonly IStatesService statesService;
        private readonly IPythonEngine pythonEngine;

        public EngineService(
            INodesService nodesService,
            ISensorsService sensorsService,
            IActuatorsService actuatorsService,
            IScenariosService scenariosService,
            IExternalActuatorsService externalActuatorsService,
            IStatesService statesService,
            IPythonEngine pythonEngine)
        {
            this.nodesService = nodesService;
            this.sensorsService = sensorsService;
            this.actuatorsService = actuatorsService;
            this.scenariosService = scenariosService;
            this.externalActuatorsService = externalActuatorsService;
            this.statesService = statesService;
            this.pythonEngine = pythonEngine;
        }

        public async Task<ScriptResult> CheckScript(string script)
        {
            var states = await statesService.GetStateDictionary();
            return await Task.FromResult(pythonEngine.CheckScript(script, states));
        }

        public async Task<List<NodeMessage>> StateChange(List<NodeMessage> nodeMessages)
        {
            List<NodeMessage> result = new List<NodeMessage>();
            if (nodeMessages.Any())
            {
                string deviceCode = nodeMessages.FirstOrDefault().NodeCode;
                await UpdateNodeStatus(deviceCode);
                foreach (NodeMessage message in nodeMessages)
                {
                    await ProcessSensorMessasge(message);
                }
                await ExecuteActions();
                result.AddRange(await GetState(deviceCode));
            }
            return result;
        }

        public async Task<List<NodeMessage>> GetState(string nodeCode)
        {
            Node node = await nodesService.GetByCode(nodeCode);
            return await ProcessActuatorMessasge(node.Actuators, nodeCode);
        }

        public async Task ExecuteActions()
        {
            var states = await statesService.GetStateDictionary();
            var scenario = await GetCurrentScenario(states);
            scenario.Actions.ForEach(async action => await ExecuteAction(action));
        }

        private async Task ExecuteAction(ActionEvent action)
        {
            var sensors = action.ExternalSensors.Select(s => new GenericSensor(s)).ToList();
            sensors.AddRange(action.Sensors.Select(s => new GenericSensor(s)).ToList());

            if (!sensors.Any() || sensors.All(s => s.Active))
            {
                var message = "";
                bool value = action.StaticState;

                if (sensors.Any())
                {
                    message = string.Join("/n", sensors.Select(s => s.Message));
                    value = true;
                }

                var periodAffected = false;
                var periodEnd = DateTime.Now;

                (periodAffected, value, periodEnd) = VerifyPeriod(action, value);

                value = action.Invert ? !value : value;

                var validActuators = action.Actuators.Where(a => !a.LockState).ToList();

                foreach (var actuator in validActuators)
                {
                    actuator.Message = message;
                    actuator.Active = value;

                    actuator.DeactivationTime = !periodAffected
                        ? DateTime.Now.AddSeconds(actuator.DefaultActiveTime)
                        : periodEnd;

                    actuator.LastStateChange = DateTime.Now;
                    await actuatorsService.Update(actuator);
                }

                foreach (var actuator in action.ExternalActuators)
                {
                    actuator.Message = message;
                    actuator.Active = value;
                    await externalActuatorsService.Update(actuator);
                }
            }
        }

        private (bool, bool, DateTime) VerifyPeriod(ActionEvent action, bool actual)
        {
            var result = actual;
            var pariodAffected = false;
            var periodEnd = DateTime.Now;
            var currentTime = new DateTime(1900, 01, 01, DateTime.Now.Hour, DateTime.Now.Minute, 0);

            if (action.ActionPeriods.Any())
            {
                result = false;
                foreach (var period in action.ActionPeriods)
                {
                    if (period.InitialTime >= currentTime && period.FinalTime <= currentTime)
                    {
                        result = true;
                        break;
                    }
                }
            }

            return (pariodAffected, result, periodEnd);
        }

        private async Task<Scenario> GetCurrentScenario(Dictionary<string, string> states)
        {
            await CheckScenarioChange(states);
            return await scenariosService.Get(int.Parse(states[StateDefaultKeys.ACTIVE_SCENARIO]));
        }

        private async Task CheckScenarioChange(Dictionary<string, string> states)
        {
            if (states[StateDefaultKeys.SCENARIO_MODE] == ScenarioMode.MANUAL) return;

            var scenarios = await scenariosService.Get();
            scenarios.OrderBy(s => s.Priority).ThenByDescending(s => s.Id);
            var currentScenario = scenarios.Find(s => s.IsDefault);
            foreach (var scenario in scenarios)
            {
                if (!scenario.IsDefault && CheckTriggers(scenario.Triggers))
                {
                    currentScenario = scenario;
                    break;
                }
            }
            if (currentScenario.Id != int.Parse(states[StateDefaultKeys.ACTIVE_SCENARIO]))
            {
                states[StateDefaultKeys.ACTIVE_SCENARIO] = currentScenario.Id.ToString();
                await statesService.UpdateState(StateDefaultKeys.ACTIVE_SCENARIO, currentScenario.Id.ToString());
            }
        }

        private bool CheckTriggers(List<ScenarioTrigger> triggers)
        {
            var result = false;

            foreach (var trigger in triggers)
            {
                if (trigger.Type == Domain.Enums.ScenarioTriggerType.Sensors)
                {
                    var sensors = trigger.ExternalSensors.Select(s => new GenericSensor(s)).ToList();
                    sensors.AddRange(trigger.Sensors.Select(s => new GenericSensor(s)).ToList());
                    result = sensors.All(s => s.Active);
                    if (result) break;
                }
                if (trigger.Type == Domain.Enums.ScenarioTriggerType.Period)
                {
                    var currentTime = new DateTime(1900, 01, 01, DateTime.Now.Hour, DateTime.Now.Minute, 0);
                    result = trigger.InitialTime <= currentTime && trigger.FinalTime >= currentTime;
                    if (result) break;
                }
            }

            return result;
        }

        private async Task UpdateNodeStatus(string deviceCode)
        {
            Node device = await nodesService.GetByCode(deviceCode);
            device.LastActivity = DateTime.Now;
            await nodesService.Update(device);
        }

        private async Task ProcessSensorMessasge(NodeMessage nodeMessage)
        {
            if (nodeMessage.Type.Equals(DeviceType.SENSOR, StringComparison.InvariantCultureIgnoreCase))
            {
                Sensor sensor = await sensorsService.GetByCode(nodeMessage.Code);
                if (sensor.Value != nodeMessage.Value)
                {
                    sensor.Value = nodeMessage.Value;
                    sensor.Active = (sensor.Value >= sensor.ActiveLowerBound && sensor.Value <= sensor.ActiveUpperBound);
                    sensor.LastStateChange = DateTime.Now;
                    sensor.Message = nodeMessage.Message;
                    await sensorsService.Update(sensor);
                }
            }
        }

        private async Task<List<NodeMessage>> ProcessActuatorMessasge(List<Actuator> actuators, string nodeCode)
        {
            List<NodeMessage> result = new List<NodeMessage>();

            foreach (var actuator in actuators)
            {
                bool expectedState = actuator.Active;
                if (actuator.DeactivationTime.HasValue)
                {
                    expectedState = actuator.DeactivationTime > DateTime.Now;
                }
                if (expectedState != actuator.Active)
                {
                    actuator.Active = expectedState;
                    actuator.DeactivationTime = !expectedState ? null : actuator.DeactivationTime;
                    await actuatorsService.Update(actuator);
                }

                result.Add(
                     new NodeMessage()
                     {
                         Code = actuator.Code,
                         Value = actuator.Active ? 1 : 0,
                         NodeCode = nodeCode,
                         Type = DeviceType.ACTUATOR,
                         Message = actuator.Message,
                         Expires = actuator.DeactivationTime
                     });
            }
            return result;
        }
    }
}