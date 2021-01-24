using System;
using HARIA.Domain.Abstractions.DTOs;

namespace HARIA.Domain.DTOs
{
    public class Log : IDTO
    {
        public int Id { get; set; }

        public DateTime Time { get; set; }

        public string Type { get; set; }

        public bool IsError { get; set; }

        public string Description { get; set; }

        public string Data { get; set; }
    }
}