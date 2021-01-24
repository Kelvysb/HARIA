using AutoMapper;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class ActionEventsService : ServiceBase<ActionEventEntity, ActionEvent>, IActionEventsService
    {
        public ActionEventsService(IActionEventsRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}