using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HARIA.Common.Helpers;
using HARIA.Domain.Abstractions.Client;
using HARIA.Domain.Constants;
using HARIA.Domain.DTOs;
using Microsoft.AspNetCore.Components;

namespace HARIA.Front.Services
{
    public class HariaServices : IHariaServices
    {
        private const string LOGGED_USER_KEY = "LOGGED_USER";

        private readonly IUsersClient usersClient;
        private readonly IEngineClient engineClient;
        private readonly INodesClient nodesClient;
        private readonly IAmbientsClient ambientsClient;
        private readonly IActionsClient actionsClient;
        private readonly IScenariosClient scenariosClient;
        private readonly IStatesClient statesClient;
        private readonly NavigationManager navManager;

        public event EventHandler<StateChangeEventArgs> StateChange;

        public event EventHandler<EventArgs> ReloadData;

        public AppState State { get; set; }

        public ILocalStorageHelper localStorageHelper { get; private set; }

        public HariaServices(
            IUsersClient usersClient,
            IEngineClient engineClient,
            INodesClient nodesClient,
            IAmbientsClient ambientsClient,
            IActionsClient actionsClient,
            ILocalStorageHelper localStorageHelper,
            IScenariosClient scenariosClient,
            IStatesClient statesClient,
            NavigationManager navManager)
        {
            this.usersClient = usersClient;
            this.engineClient = engineClient;
            this.nodesClient = nodesClient;
            this.ambientsClient = ambientsClient;
            this.actionsClient = actionsClient;
            this.localStorageHelper = localStorageHelper;
            this.navManager = navManager;
            this.scenariosClient = scenariosClient;
            this.statesClient = statesClient;

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

        public void NotifyChange()
        {
            RaiseNotifyChange(this, new StateChangeEventArgs("HariaServices", this));
        }

        private void RaiseNotifyChange(object sender, StateChangeEventArgs e)
        {
            EventHandler<StateChangeEventArgs> handler = StateChange;
            handler?.Invoke(sender, e);
        }

        public Task<List<Ambient>> GetAmbients()
        {
            if (State.LoggedUser == null) return null;
            return ambientsClient.Get(State.LoggedUser?.Token);
        }

        public Task<List<Node>> GetNodes()
        {
            if (State.LoggedUser == null) return null;
            return nodesClient.Get(State.LoggedUser?.Token);
        }

        public Task<List<Scenario>> GetScenarios()
        {
            if (State.LoggedUser == null) return null;
            return scenariosClient.Get(State.LoggedUser?.Token);
        }

        public Task<List<State>> GetStates()
        {
            if (State.LoggedUser == null) return null;
            return statesClient.Get(State.LoggedUser?.Token);
        }

        public async Task<List<NodeMessage>> SendNodeMessage(Node node)
        {
            if (State.LoggedUser == null) return null;
            List<NodeMessage> message = node.Sensors.Select(s =>
            {
                return new NodeMessage()
                {
                    Code = s.Code,
                    NodeCode = node.Code,
                    Type = DeviceType.SENSOR,
                    Message = s.Message,
                    Value = s.Value
                };
            }).ToList();
            return await engineClient.StateChange(message, State.LoggedUser.Token);
        }

        public Task<List<NodeMessage>> GetNodeStatus(string code)
        {
            if (State.LoggedUser == null) return null;
            return engineClient.GetState(code, State.LoggedUser.Token);
        }

        public void RequestReload()
        {
            EventHandler<EventArgs> handler = ReloadData;
            handler?.Invoke(this, new EventArgs());
        }
    }
}