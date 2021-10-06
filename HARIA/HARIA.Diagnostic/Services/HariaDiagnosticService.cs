using HARIA.Common.Abstractions;
using HARIA.Diagnostic.Abstractions;
using HARIA.Domain.Abstractions.Services;
using HARIA.Services;
using Microsoft.AspNetCore.Components;

namespace HARIA.Diagnostic.Services
{
    public class HariaDiagnosticService : HariaServiceBase, IHariaDiagnosticService
    {
        public HariaDiagnosticService(
            NavigationManager navManager,
            ILocalStorageHelper localStorage,
            IMqttService mqttService,
            IUsersService usersService)
            : base(navManager, localStorage, usersService)
        {
            MqttService = mqttService;
        }

        public IMqttService MqttService { get; set; }
    }
}