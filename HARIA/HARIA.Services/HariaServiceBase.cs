using System;
using System.Threading.Tasks;
using HARIA.Common.Abstractions;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Events;
using HARIA.Domain.Models;
using Microsoft.AspNetCore.Components;

namespace HARIA.Services
{
    public abstract class HariaServiceBase : IHariaServiceBase
    {
        protected const string LOGGED_USER_KEY = "LOGGED_USER";
        protected readonly NavigationManager navManager;
        protected readonly IUsersService usersService;

        public HariaServiceBase(
           NavigationManager navManager,
           ILocalStorageHelper localStorage,
           IUsersService usersService)
        {
            this.navManager = navManager;
            this.usersService = usersService;
            LocalStorage = localStorage;
            State = new AppState();
            State.StateChange += RaiseNotifyChange;
        }

        public event EventHandler<StateChangeEventArgs> StateChange;

        public AppState State { get; set; }

        protected ILocalStorageHelper LocalStorage { get; private set; }

        public virtual void HandleError(Exception exception)
        {
            State.CurrentError = exception;
            navManager.NavigateTo("/error");
        }

        public virtual async Task CheckLoggedUser()
        {
            State.LoggedUser = await LocalStorage.GetItem<User>(LOGGED_USER_KEY);
        }

        public virtual async Task Login(string name, string passwordHash)
        {
            var user = new User
            {
                Login = name,
                Password = passwordHash
            };
            State.LoggedUser = await usersService.Login(user);
            await LocalStorage.SetItem(LOGGED_USER_KEY, State.LoggedUser);
        }

        public virtual async Task LogOut()
        {
            if (State.LoggedUser != null)
            {
                State.LoggedUser = null;
                await LocalStorage.RemoveItem(LOGGED_USER_KEY);
            }
        }

        protected virtual void RaiseNotifyChange(object sender, StateChangeEventArgs e)
        {
            EventHandler<StateChangeEventArgs> handler = StateChange;
            handler?.Invoke(sender, e);
        }
    }
}