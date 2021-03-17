using System;

namespace HARIA.Front.Services
{
    public class StateChangeEventArgs : EventArgs
    {
        public StateChangeEventArgs(string state, object value)
        {
            State = state;
            Value = value;
        }

        public string State { get; private set; }

        public object Value { get; private set; }
    }
}