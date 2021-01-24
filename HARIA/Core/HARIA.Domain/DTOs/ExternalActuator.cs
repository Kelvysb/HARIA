﻿using System;
using HARIA.Domain.Abstractions.DTOs;

namespace HARIA.Domain.DTOs
{
    public class ExternalActuator : IDTO
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Script { get; set; }

        public DateTime LastExecution { get; set; }
    }
}