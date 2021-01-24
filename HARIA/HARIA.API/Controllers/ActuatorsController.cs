﻿using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;

namespace HARIA.API.Controllers
{
    public class ActuatorsController : ControllerBase<Actuator>
    {
        public ActuatorsController(IActuatorsService service) : base(service)
        {
        }
    }
}