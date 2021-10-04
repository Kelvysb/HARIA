using System;
using System.Threading.Tasks;
using HARIA.Domain.Models;

namespace HARIA.Domain.Abstractions.Services
{
    public interface IMqttService
    {
        Task Subscribe(Action<DeviceData> receiveMessage);

        Task Unsubscribe();

        Task StartPublisher();

        Task SendMessage(string payload, string topic);
    }
}