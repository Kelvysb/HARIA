using HARIA.Domain.Attributes;
using System;
using System.Text.Json.Serialization;

namespace HARIA.Domain.Entities
{
    public class DigitalElementEntity
    {
        [JsonPropertyName("value")]
        public bool Value { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class AnalogElementEntity
    {
        [JsonPropertyName("value")]
        public double Value { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class IoGroupEntity
    {
        [JsonPropertyName("d0")]
        public DigitalElementEntity D0 { get; set; }

        [JsonPropertyName("d1")]
        public DigitalElementEntity D1 { get; set; }

        [JsonPropertyName("d2")]
        public DigitalElementEntity D2 { get; set; }

        [JsonPropertyName("d3")]
        public DigitalElementEntity D3 { get; set; }

        [JsonPropertyName("d4")]
        public DigitalElementEntity D4 { get; set; }

        [JsonPropertyName("d5")]
        public DigitalElementEntity D5 { get; set; }

        [JsonPropertyName("d6")]
        public DigitalElementEntity D6 { get; set; }

        [JsonPropertyName("d7")]
        public DigitalElementEntity D7 { get; set; }

        [JsonPropertyName("d8")]
        public DigitalElementEntity D8 { get; set; }

        [JsonPropertyName("a0")]
        public AnalogElementEntity A0 { get; set; }
    }

    [Collection("Devices")]
    public class DeviceDataEntity : EntityBase
    {
        [JsonPropertyName("device_name")]
        public string DeviceName { get; set; }

        [JsonPropertyName("sensors")]
        public IoGroupEntity Sensors { get; set; }

        [JsonPropertyName("actuators")]
        public IoGroupEntity Actuators { get; set; }

        [JsonPropertyName("datetime")]
        public DateTime Datetime { get; set; }
    }
}