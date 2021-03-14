using HARIA.Domain.Abstractions.DTOs;

namespace HARIA.Domain.DTOs
{
    public class GenericSensor : IGenericSensor
    {
        public GenericSensor(IGenericSensor sensor)
        {
            Active = sensor.Active;
            Message = sensor.Message;
        }

        public bool Active { get; set; }

        public string Message { get; set; }
    }
}