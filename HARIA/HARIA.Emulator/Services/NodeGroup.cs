using System;
using System.Collections.Generic;
using System.Threading;
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
                async (object stateInfo) => UpdateActuators(await hariaServices.GetNodeStatus(Node.Code)),
                new AutoResetEvent(false),
                0,
                INTERVAL);
        }

        ~NodeGroup()
        {
            Dispose();
        }

        public void Dispose()
        {
            Running.Dispose();
        }

        public Node Node { get; set; }

        public void NotifyNode(object sender, List<NodeMessage> messages)
        {
            EventHandler<MessageReceiveEventArgs> handler = MessageReceived;
            handler?.Invoke(sender, new MessageReceiveEventArgs(messages));
        }

        public void UpdateActuators(List<NodeMessage> messages)
        {
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