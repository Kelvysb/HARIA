using System;
using System.Threading.Tasks;
using HARIA.Diagnostic.Services;
using HARIA.Domain.Abstractions;
using HARIA.Domain.Abstractions.Services;

namespace HARIA.Diagnostic.Abstractions
{
    public interface IHariaDiagnosticService
    {
        event EventHandler<StateChangeEventArgs> StateChange;

        AppState State { get; set; }

        public IMqttService MqttService { get; set; }

        ILocalStorageHelper LocalStorage { get; }

        void HandleError(Exception exception);

        Task CheckLoggedUser();

        Task Login(string name, string passwordHash);

        Task LogOut();
    }
}