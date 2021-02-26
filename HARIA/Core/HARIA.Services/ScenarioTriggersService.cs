﻿using AutoMapper;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Services
{
    public class ScenarioTriggersService : ServiceBase<ScenarioTriggerEntity, ScenarioTrigger>, IScenarioTriggersService
    {
        public ScenarioTriggersService(IScenarioTriggersRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}