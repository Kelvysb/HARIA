using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Domain.DTOs;
using HARIA.Emulator.Helpers;

namespace HARIA.Emulator.Services
{
    public interface IHariaServices
    {
        event EventHandler<StateChangeEventArgs> StateChange;

        event EventHandler<EventArgs> ReloadData;

        public AppState State { get; set; }

        ILocalStorageHelper LocalStorage { get; }

        Task CheckLoggedUser();

        Task Login(string name, string passwordHash);

        Task LogOut();

        Task<List<Ambient>> GetAmbients();

        Task<List<Node>> GetNodes();

        Task AddDefaultData(I18nText.DefaultData translate);

        void HandleError(Exception exception);

        Task<List<NodeMessage>> SendNodeMessage(Node node);

        Task<List<NodeMessage>> GetNodeStatus(string code);

        void NotifyChange();

        void RequestReload();
    }
}