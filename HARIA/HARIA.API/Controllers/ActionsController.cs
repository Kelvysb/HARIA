﻿using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class ActionsController : ControllerBase<Action>
    {
        public ActionsController(IServiceBase<Action> service) : base(service)
        {
        }
    }
}