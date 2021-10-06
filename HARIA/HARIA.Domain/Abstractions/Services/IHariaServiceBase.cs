using System;
using System.Threading.Tasks;
using HARIA.Domain.Events;
using HARIA.Domain.Models;

namespace HARIA.Domain.Abstractions.Services
{
    public interface IHariaServiceBase
    {
        event EventHandler<StateChangeEventArgs> StateChange;

        AppState State { get; set; }

        Task CheckLoggedUser();

        void HandleError(Exception exception);

        Task Login(string name, string passwordHash);

        Task LogOut();
    }
}