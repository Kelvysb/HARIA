using System;
using System.Collections.Generic;
using HARIA.Domain.DTOs;

namespace HARIA.Front.Services
{
    public class AppState
    {
        private string currentLocation = "";
        private bool menuPinned = false;
        private Exception currentError = null;
        private User loggedUser = null;
        private List<Ambient> ambients = new List<Ambient>();

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

        public bool MenuPinned
        {
            get => menuPinned;
            set
            {
                menuPinned = value;
                NotifyChange("MenuPinned", menuPinned);
            }
        }

        public List<Ambient> Ambients
        {
            get => ambients;
            set
            {
                ambients = value;
                NotifyChange("Ambients", ambients);
            }
        }

        private void NotifyChange(string state, object value)
        {
            EventHandler<StateChangeEventArgs> handler = StateChange;
            handler?.Invoke(this, new StateChangeEventArgs(state, value));
        }
    }
}