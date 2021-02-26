using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HARIA.Client.Extenssions;
using HARIA.Domain.Abstractions.Client;
using HARIA.Domain.Configuration;
using HARIA.Domain.DTOs;

namespace HARIA.Client
{
    public class EngineClient : IEngineClient
    {
        private HttpClient httpClient;
        private const string api = "Engine";

        public EngineClient(HariaApiConfig hariaApiConfig)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(hariaApiConfig.Url);
        }

        public async Task<List<DeviceMessage>> StateChange(List<DeviceMessage> deviceMessages, string token)
        {
            var content = JsonContent.Create(deviceMessages);
            var response = await httpClient
                .AddJWT(token)
                .PostAsync(api, content);
            return await response.CheckResult<List<DeviceMessage>>();
        }

        public async Task<List<DeviceMessage>> GetState(string deviceCode, string token)
        {
            var response = await httpClient
                .AddJWT(token)
                .GetAsync(api);
            return await response.CheckResult<List<DeviceMessage>>();
        }
    }
}