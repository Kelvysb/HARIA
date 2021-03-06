using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Domain.DTOs;

namespace HARIA.Emulator.Services
{
    public interface IHariaServices
    {
        event EventHandler<StateChangeEventArgs> StateChange;

        public AppState State { get; set; }

        Task CheckLoggedUser();

        Task Login(string name, string passwordHash);

        Task LogOut();

        Task<List<Device>> GetDevices();

        Task AddDefaultDevices(I18nText.DefaultData translate);

        void HandleError(Exception exception);
    }
}