﻿using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;

namespace HARIA.DataAccess
{
    public class SensorsRepository : RepositoryBase<SensorEntity>, ISensorsRepository
    {
        public SensorsRepository(IContext context) : base(context)
        {
        }
    }
}