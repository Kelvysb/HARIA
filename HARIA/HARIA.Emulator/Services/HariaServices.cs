using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Client;
using HARIA.Domain.DTOs;
using HARIA.Emulator.Helpers;
using Microsoft.AspNetCore.Components;

namespace HARIA.Emulator.Services
{
    public class HariaServices : IHariaServices
    {
        private const string LOGGED_USER_KEY = "LOGGED_USER";

        private readonly IUsersClient usersClient;
        private readonly IEngineClient engineClient;
        private readonly IDevicesClient devicesClient;
        private readonly IActuatorsClient actuatorsClient;
        private readonly ISensorsClient sensorsClient;
        private readonly IAmbientsClient ambientsClient;
        private readonly IActionsClient actionsClient;
        private readonly ILocalStorageHelper localStorageHelper;
        private readonly NavigationManager navManager;

        public event EventHandler<StateChangeEventArgs> StateChange;

        public AppState State { get; set; }

        public HariaServices(
            IUsersClient usersClient,
            IEngineClient engineClient,
            IDevicesClient devicesClient,
            IActuatorsClient actuatorsClient,
            ISensorsClient sensorsClient,
            IAmbientsClient ambientsClient,
            IActionsClient actionsClient,
            ILocalStorageHelper localStorageHelper,
            NavigationManager navManager)
        {
            this.usersClient = usersClient;
            this.engineClient = engineClient;
            this.devicesClient = devicesClient;
            this.actuatorsClient = actuatorsClient;
            this.sensorsClient = sensorsClient;
            this.ambientsClient = ambientsClient;
            this.actionsClient = actionsClient;
            this.localStorageHelper = localStorageHelper;
            this.navManager = navManager;

            State = new AppState();
            State.StateChange += RaiseNotifyChange;
        }

        public async Task Login(string name, string passwordHash)
        {
            var user = new User()
            {
                Name = name,
                PasswordHash = passwordHash
            };
            State.LoggedUser = await usersClient.Login(user);
            await localStorageHelper.SetItem(LOGGED_USER_KEY, State.LoggedUser);
        }

        public async Task LogOut()
        {
            if (State.LoggedUser != null)
            {
                State.LoggedUser = null;
                await localStorageHelper.RemoveItem(LOGGED_USER_KEY);
            }
        }

        public async Task CheckLoggedUser()
        {
            State.LoggedUser = await localStorageHelper.GetItem<User>(LOGGED_USER_KEY);
        }

        public void HandleError(Exception exception)
        {
            State.CurrentError = exception;
            navManager.NavigateTo("/error");
        }

        private void NotifyChange()
        {
            RaiseNotifyChange(this, new StateChangeEventArgs("HariaServices", this));
        }

        private void RaiseNotifyChange(object sender, StateChangeEventArgs e)
        {
            EventHandler<StateChangeEventArgs> handler = StateChange;
            handler?.Invoke(sender, e);
        }

        public Task<List<Device>> GetDevices()
        {
            return devicesClient.Get(State.LoggedUser?.Token);
        }

        public async Task AddDefaultDevices(I18nText.DefaultData translate)
        {
            var defaultData = DefaultDataHelper.GetDefaultData(translate);
            var ambients = defaultData.Item1;
            var devices = defaultData.Item2;
            var actions = defaultData.Item3;

            foreach (var ambient in ambients)
            {
                await ambientsClient.Add(ambient, State.LoggedUser.Token);
            }

            foreach (var device in devices)
            {
                await devicesClient.Add(device, State.LoggedUser.Token);
            }

            foreach (var action in actions)
            {
                await actionsClient.Add(action, State.LoggedUser.Token);
            }
        }
    }
}