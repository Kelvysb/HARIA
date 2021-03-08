using System;
using HARIA.Domain.Abstractions.Entities;

namespace HARIA.Domain.Entities
{
    public class ActionEventPeriodEntity : IEntity
    {
        public int Id { get; set; }

        public int ActionEventId { get; set; }

        public ActionEventEntity ActionEvent { get; set; }

        public DateTime InitialTime { get; set; }

        public DateTime FinalTime { get; set; }
    }
}