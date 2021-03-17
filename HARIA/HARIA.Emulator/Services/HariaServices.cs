using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HARIA.Common.Helpers;
using HARIA.Domain.Abstractions.Client;
using HARIA.Domain.Constants;
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
        private readonly INodesClient nodesClient;
        private readonly IAmbientsClient ambientsClient;
        private readonly IActionsClient actionsClient;
        private readonly NavigationManager navManager;

        public event EventHandler<StateChangeEventArgs> StateChange;

        public event EventHandler<EventArgs> ReloadData;

        public AppState State { get; set; }

        public ILocalStorageHelper LocalStorage { get; private set; }

        public HariaServices(
            IUsersClient usersClient,
            IEngineClient engineClient,
            INodesClient devicesClient,
            IAmbientsClient ambientsClient,
            IActionsClient actionsClient,
            ILocalStorageHelper localStorageHelper,
            NavigationManager navManager)
        {
            this.usersClient = usersClient;
            this.engineClient = engineClient;
            this.nodesClient = devicesClient;
            this.ambientsClient = ambientsClient;
            this.actionsClient = actionsClient;
            this.LocalStorage = localStorageHelper;
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
            await LocalStorage.SetItem(LOGGED_USER_KEY, State.LoggedUser);
        }

        public async Task LogOut()
        {
            if (State.LoggedUser != null)
            {
                State.NodeGroups.ForEach(n => n.Dispose());
                State.NodeGroups = new List<NodeGroup>();
                State.Ambients = new List<Ambient>();
                State.LoggedUser = null;
                await LocalStorage.RemoveItem(LOGGED_USER_KEY);
            }
        }

        public async Task CheckLoggedUser()
        {
            State.LoggedUser = await LocalStorage.GetItem<User>(LOGGED_USER_KEY);
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

        public async Task AddDefaultData(I18nText.DefaultData translate)
        {
            if (State.LoggedUser == null) return;
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
                await nodesClient.Add(device, State.LoggedUser.Token);
            }

            foreach (var action in actions)
            {
                await actionsClient.Add(action, State.LoggedUser.Token);
            }
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