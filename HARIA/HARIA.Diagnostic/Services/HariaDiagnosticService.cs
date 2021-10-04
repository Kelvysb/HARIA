using System;
using System.Threading.Tasks;
using HARIA.Diagnostic.Abstractions;
using HARIA.Domain.Abstractions;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Models;
using Microsoft.AspNetCore.Components;

namespace HARIA.Diagnostic.Services
{
    public class HariaDiagnosticService : IHariaDiagnosticService
    {
        private const string LOGGED_USER_KEY = "LOGGED_USER";
        private readonly NavigationManager navManager;
        private readonly IUsersService usersService;

        public HariaDiagnosticService(
            NavigationManager navManager,
            ILocalStorageHelper localStorage,
            IMqttService mqttService,
            IUsersService usersService)
        {
            this.navManager = navManager;
            this.usersService = usersService;
            LocalStorage = localStorage;
            MqttService = mqttService;
            State = new AppState();
            State.StateChange += RaiseNotifyChange;
        }

        public event EventHandler<StateChangeEventArgs> StateChange;

        public ILocalStorageHelper LocalStorage { get; private set; }

        public AppState State { get; set; }

        public IMqttService MqttService { get; set; }

        public void HandleError(Exception exception)
        {
            State.CurrentError = exception;
            navManager.NavigateTo("/error");
        }

        public async Task CheckLoggedUser()
        {
            State.LoggedUser = await LocalStorage.GetItem<User>(LOGGED_USER_KEY);
        }

        public async Task Login(string name, string passwordHash)
        {
            var user = new User
            {
                Login = name,
                Password = passwordHash
            };
            State.LoggedUser = await usersService.Login(user);
            await LocalStorage.SetItem(LOGGED_USER_KEY, State.LoggedUser);
        }

        public async Task LogOut()
        {
            if (State.LoggedUser != null)
            {
                State.LoggedUser = null;
                await LocalStorage.RemoveItem(LOGGED_USER_KEY);
            }
        }

        private void RaiseNotifyChange(object sender, StateChangeEventArgs e)
        {
            EventHandler<StateChangeEventArgs> handler = StateChange;
            handler?.Invoke(sender, e);
        }
    }
}