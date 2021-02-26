using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HARIA.Client.Extenssions;
using HARIA.Domain.Abstractions.Client;
using HARIA.Domain.Abstractions.DTOs;
using HARIA.Domain.Configuration;

namespace HARIA.Client
{
    public abstract class ClientBase<TDTO> : IClientBase<TDTO>
        where TDTO : class, IDTO, new()
    {
        protected HttpClient httpClient;
        protected string api;

        protected ClientBase(HariaApiConfig hariaApiConfig, string api)
        {
            this.api = api;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(hariaApiConfig.Url);
        }

        public virtual async Task<bool> Add(TDTO dto, string token)
        {
            var content = JsonContent.Create(dto);
            var response = await httpClient
                .AddJWT(token)
                .PostAsync(api, content);
            return response.CheckResult();
        }

        public virtual async Task<bool> Delete(int id, string token)
        {
            var response = await httpClient
                .AddJWT(token)
                .DeleteAsync($"{api}/{id}");
            return response.CheckResult();
        }

        public virtual async Task<List<TDTO>> Get(string token)
        {
            var response = await httpClient
                .AddJWT(token)
                .GetAsync(api);
            return await response.CheckResult<List<TDTO>>();
        }

        public virtual async Task<TDTO> Get(int id, string token)
        {
            var response = await httpClient
                .AddJWT(token)
                .GetAsync($"{api}/{id}");
            return await response.CheckResult<TDTO>();
        }

        public virtual async Task<bool> Update(TDTO dto, string token)
        {
            var content = JsonContent.Create(dto);
            var response = await httpClient
               .AddJWT(token)
               .PutAsync(api, content);
            return response.CheckResult();
        }
    }
}