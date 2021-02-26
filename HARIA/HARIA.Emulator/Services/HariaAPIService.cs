using System;
using System.Net.Http;
using HARIA.Domain.Configuration;

namespace HARIA.Emulator.Services
{
    public class HariaAPIService
    {
        private readonly HttpClient httpclient;

        public HariaAPIService(HariaApiConfig hariaApiConfig)
        {
            httpclient = new HttpClient();
            httpclient.BaseAddress = new Uri(hariaApiConfig.Url);
        }
    }
}