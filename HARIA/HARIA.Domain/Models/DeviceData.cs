using System;
using System.Text.Json.Serialization;

namespace HARIA.Domain.Models
{
    public class DigitalElement
    {
        [JsonPropertyName("value")]
        public bool Value { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class AnalogElement
    {
        [JsonPropertyName("value")]
        public double Value { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class IoGroup
    {
        [JsonPropertyName("d0")]
        public DigitalElement D0 { get; set; }

        [JsonPropertyName("d1")]
        public DigitalElement D1 { get; set; }

        [JsonPropertyName("d2")]
        public DigitalElement D2 { get; set; }

        [JsonPropertyName("d3")]
        public DigitalElement D3 { get; set; }

        [JsonPropertyName("d4")]
        public DigitalElement D4 { get; set; }

        [JsonPropertyName("d5")]
        public DigitalElement D5 { get; set; }

        [JsonPropertyName("d6")]
        public DigitalElement D6 { get; set; }

        [JsonPropertyName("d7")]
        public DigitalElement D7 { get; set; }

        [JsonPropertyName("d8")]
        public DigitalElement D8 { get; set; }

        [JsonPropertyName("a0")]
        public AnalogElement A0 { get; set; }
    }

    public class DeviceData
    {
        [JsonPropertyName("device_id")]
        public string DeviceId { get; set; }

        [JsonPropertyName("device_name")]
        public string DeviceName { get; set; }

        [JsonPropertyName("sensors")]
        public IoGroup Sensors { get; set; }

        [JsonPropertyName("actuators")]
        public IoGroup Actuators { get; set; }

        [JsonPropertyName("datetime")]
        public DateTime Datetime { get; set; }
    }
}