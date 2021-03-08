using System;
using HARIA.Domain.Abstractions.DTOs;

namespace HARIA.Domain.DTOs
{
    public class ActionEventPeriod : IDTO
    {
        public int Id { get; set; }

        public int ActionEventId { get; set; }

        public DateTime InitialTime { get; set; }

        public DateTime FinalTime { get; set; }
    }
}