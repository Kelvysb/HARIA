using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Domain.DTOs;

namespace HARIA.Domain.Abstractions.Client
{
    public interface IEngineClient
    {
        Task<List<NodeMessage>> StateChange(List<NodeMessage> deviceMessages, string token);

        Task<List<NodeMessage>> GetState(string deviceCode, string token);
    }
}