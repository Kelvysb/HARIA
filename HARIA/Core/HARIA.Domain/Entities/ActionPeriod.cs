using System;
using HARIA.Domain.Abstractions.Entities;

namespace HARIA.Domain.Entities
{
    public class ActionPeriod : IEntity
    {
        public int Id { get; set; }

        public int ActionId { get; set; }

        public DateTime InitialTime { get; set; }

        public DateTime FinalTime { get; set; }
    }
}