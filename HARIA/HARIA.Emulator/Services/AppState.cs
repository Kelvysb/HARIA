using System;
using HARIA.Domain.DTOs;

namespace HARIA.Emulator.Services
{
    public class AppState
    {
        private string currentLocation = "";
        private Exception currentError = null;
        private User loggedUser = null;

        public event EventHandler<StateChangeEventArgs> StateChange;

        public Exception CurrentError
        {
            get => currentError;
            set
            {
                currentError = value;
                NotifyChange("CurrentError", currentError);
            }
        }

        public string CurrentLocation
        {
            get => currentLocation;
            set
            {
                currentLocation = value;
                NotifyChange("CurrentLocation", currentLocation);
            }
        }

        public User LoggedUser
        {
            get => loggedUser;
            set
            {
                loggedUser = value;
                NotifyChange("LoggedUser", loggedUser);
            }
        }

        private void NotifyChange(string state, object value)
        {
            EventHandler<StateChangeEventArgs> handler = StateChange;
            handler?.Invoke(this, new StateChangeEventArgs(state, value));
        }
    }
}