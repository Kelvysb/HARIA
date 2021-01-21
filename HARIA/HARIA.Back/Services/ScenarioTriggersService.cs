﻿using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.Core.Services
{
    public class ScenarioTriggersService : ServiceBase<ScenarioTrigger>, IScenarioTriggersService
    {
        public ScenarioTriggersService(IScenarioTriggersRepository repository) : base(repository)
        {
        }
    }
}