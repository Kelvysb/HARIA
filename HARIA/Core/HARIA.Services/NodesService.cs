using System.Threading.Tasks;
using AutoMapper;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class NodesService : ServiceBase<NodeEntity, Node>, INodesService
    {
        private INodesRepository devicesRepository;

        public NodesService(INodesRepository repository, IMapper mapper) : base(repository, mapper)
        {
            devicesRepository = repository;
        }

        public async Task<Node> GetByCode(string code)
        {
            NodeEntity entity = await devicesRepository.GetByCode(code);
            return mapper.Map<Node>(entity);
        }
    }
}