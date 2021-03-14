using System;
using System.Collections.Generic;
using HARIA.Domain.DTOs;

namespace HARIA.Emulator.Services
{
    public class MessageReceiveEventArgs : EventArgs
    {
        public List<NodeMessage> Messages { get; private set; }

        public MessageReceiveEventArgs(List<NodeMessage> messages)
        {
            Messages = messages;
        }
    }
}