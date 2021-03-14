using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Domain.DTOs;

namespace HARIA.Domain.Abstractions.Services
{
    public interface IEngineService
    {
        Task<List<NodeMessage>> StateChange(List<NodeMessage> nodeMessages);

        Task<List<NodeMessage>> GetState(string nodeCode);

        Task ExecuteActions();

        Task<ScriptResult> CheckScript(string script);
    }
}