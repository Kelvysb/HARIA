using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HARIA.Domain.Models
{
    public class DigitalElement
    {
        [JsonPropertyName("value")]
        public bool Value { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            return obj is DigitalElement element &&
                   Value == element.Value &&
                   Description == element.Description;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Description);
        }
    }

    public class AnalogElement
    {
        [JsonPropertyName("value")]
        public double Value { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            return obj is AnalogElement element &&
                   Value == element.Value &&
                   Description == element.Description;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Description);
        }
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

        public override bool Equals(object obj)
        {
            return obj is IoGroup group &&
                   EqualityComparer<DigitalElement>.Default.Equals(D0, group.D0) &&
                   EqualityComparer<DigitalElement>.Default.Equals(D1, group.D1) &&
                   EqualityComparer<DigitalElement>.Default.Equals(D2, group.D2) &&
                   EqualityComparer<DigitalElement>.Default.Equals(D3, group.D3) &&
                   EqualityComparer<DigitalElement>.Default.Equals(D4, group.D4) &&
                   EqualityComparer<DigitalElement>.Default.Equals(D5, group.D5) &&
                   EqualityComparer<DigitalElement>.Default.Equals(D6, group.D6) &&
                   EqualityComparer<DigitalElement>.Default.Equals(D7, group.D7) &&
                   EqualityComparer<DigitalElement>.Default.Equals(D8, group.D8) &&
                   EqualityComparer<AnalogElement>.Default.Equals(A0, group.A0);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(D0);
            hash.Add(D1);
            hash.Add(D2);
            hash.Add(D3);
            hash.Add(D4);
            hash.Add(D5);
            hash.Add(D6);
            hash.Add(D7);
            hash.Add(D8);
            hash.Add(A0);
            return hash.ToHashCode();
        }
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

        public override bool Equals(object obj)
        {
            return obj is DeviceData data &&
                   DeviceId == data.DeviceId &&
                   DeviceName == data.DeviceName &&
                   EqualityComparer<IoGroup>.Default.Equals(Sensors, data.Sensors) &&
                   EqualityComparer<IoGroup>.Default.Equals(Actuators, data.Actuators);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DeviceId, DeviceName, Sensors, Actuators);
        }
    }
}