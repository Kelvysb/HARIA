namespace HARIA.Domain.Abstractions.DTOs
{
    public interface IGenericSensor
    {
        public bool Active { get; set; }

        public string Message { get; set; }
    }
}