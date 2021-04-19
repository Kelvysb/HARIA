using System;
using System.Collections.Generic;
using System.Linq;
using HARIA.Domain.DTOs;

namespace HARIA.Front.Services
{
    public class AppState
    {
        private string currentLocation = "";
        private bool menuPinned = false;
        private Exception currentError = null;
        private User loggedUser = null;
        private List<State> stateTable = new List<State>();
        private List<Ambient> ambients = new List<Ambient>();
        private List<Scenario> scenarios = new List<Scenario>();
        private int currentScenarioId = 0;
        private bool scenarioManual = false;

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

        public List<Scenario> Scenarios
        {
            get => scenarios;
            set
            {
                scenarios = value;
                NotifyChange("Scenarios", scenarios);
            }
        }

        public int CurrentScenarioId
        {
            get => currentScenarioId;
            set
            {
                currentScenarioId = value;
                NotifyChange("CurrentScenarioId", currentScenarioId);
            }
        }

        public bool ScenarioManual
        {
            get => scenarioManual;
            set
            {
                scenarioManual = value;
                NotifyChange("ScenarioManual", scenarioManual);
            }
        }

        public List<State> StateTable
        {
            get => stateTable;
            set
            {
                stateTable = value;
                NotifyChange("StateTable", stateTable);
            }
        }

        public string GetStateValue(string key)
        {
            if (!stateTable.Any()) return null;
            return stateTable
                .Where(s => s.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase))
                .Select(s => s.Value != null ? s.Value : s.DefaultValue)
                .FirstOrDefault();
        }

        private void NotifyChange(string state, object value)
        {
            EventHandler<StateChangeEventArgs> handler = StateChange;
            handler?.Invoke(this, new StateChangeEventArgs(state, value));
        }
    }
}