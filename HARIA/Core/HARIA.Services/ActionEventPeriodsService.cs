using AutoMapper;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class ActionEventPeriodsService : ServiceBase<ActionEventPeriodEntity, ActionEventPeriod>, IActionEventPeriodsService
    {
        public ActionEventPeriodsService(IActionEventPeriodsRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}