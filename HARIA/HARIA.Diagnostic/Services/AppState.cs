using System;
using HARIA.Domain.Models;

namespace HARIA.Diagnostic.Services
{
    public class AppState
    {
        private User loggedUser = null;
        private Exception currentError = null;
        private string currentLocation = "";

        public event EventHandler<StateChangeEventArgs> StateChange;

        public User LoggedUser
        {
            get => loggedUser;
            set
            {
                loggedUser = value;
                NotifyChange("LoggedUser", loggedUser);
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

        public Exception CurrentError
        {
            get => currentError;
            set
            {
                currentError = value;
                NotifyChange("CurrentError", currentError);
            }
        }

        private void NotifyChange(string state, object value)
        {
            EventHandler<StateChangeEventArgs> handler = StateChange;
            handler?.Invoke(this, new StateChangeEventArgs(state, value));
        }
    }
}