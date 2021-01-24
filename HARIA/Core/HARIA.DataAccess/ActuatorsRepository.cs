﻿using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.DataAccess
{
    public class ActuatorsRepository : RepositoryBase<ActuatorEntity>, IActuatorsRepository
    {
        public ActuatorsRepository(IContext context) : base(context)
        {
        }
    }
}