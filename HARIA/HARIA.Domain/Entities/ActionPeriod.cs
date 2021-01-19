using System;

namespace HARIA.Domain.Entities
{
    public class ActionPeriod
    {
        public int Id { get; set; }

        public int ActionId { get; set; }

        public DateTime InitialTime { get; set; }

        public DateTime FinalTime { get; set; }
    }
}