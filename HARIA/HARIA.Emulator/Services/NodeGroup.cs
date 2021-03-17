using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HARIA.Domain.Constants;
using HARIA.Domain.DTOs;

namespace HARIA.Emulator.Services
{
    public class NodeGroup : IDisposable
    {
        private static int INTERVAL = 1000;
        private Timer Running;
        private IHariaServices hariaServices;

        public event EventHandler<MessageReceiveEventArgs> MessageReceived;

        public NodeGroup(Node node, IHariaServices hariaServices)
        {
            Node = node;
            this.hariaServices = hariaServices;
            Running = new Timer(
                    async (object stateInfo) => await TimerExecution(stateInfo),
                    new AutoResetEvent(false),
                    0,
                    INTERVAL);
        }

        private async Task TimerExecution(object stateInfo)
        {
            if (hariaServices.State.LoggedUser == null)
            {
                Dispose();
                return;
            }
            UpdateActuators(await hariaServices.GetNodeStatus(Node.Code));
        }

        public void Dispose()
        {
            Running.Dispose();
            Running = null;
        }

        public Node Node { get; set; }

        public void NotifyNode(object sender, List<NodeMessage> messages)
        {
            EventHandler<MessageReceiveEventArgs> handler = MessageReceived;
            handler?.Invoke(sender, new MessageReceiveEventArgs(messages));
        }

        public void UpdateActuators(List<NodeMessage> messages)
        {
            if (hariaServices.State.LoggedUser == null) return;
            var anyChanges = false;
            foreach (var message in messages)
            {
                if (message.Type.Equals(DeviceType.ACTUATOR))
                {
                    var actuator = Node.Actuators.Find(a => a.Code.Equals(message.Code));
                    if (actuator != null && (actuator.Active != (message.Value != 0) || actuator.Message != message.Message || actuator.DeactivationTime != message.Expires))
                    {
                        actuator.LastStateChange = DateTime.Now;
                        actuator.Active = message.Value != 0;
                        actuator.Message = message.Message;
                        actuator.DeactivationTime = message.Expires;
                        anyChanges = true;
                    }
                }
            }
            if (anyChanges)
            {
                hariaServices.NotifyChange();
            }
        }
    }
}