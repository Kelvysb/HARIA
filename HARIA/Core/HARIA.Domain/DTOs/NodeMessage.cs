using System;

namespace HARIA.Domain.DTOs
{
    public class NodeMessage
    {
        public string NodeCode { get; set; }

        public string Code { get; set; }

        public string Type { get; set; }

        public int Value { get; set; }

        public string Message { get; set; }

        public DateTime? Expires { get; set; }
    }
}