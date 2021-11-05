using HARIA.Script.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARIA.Script.Models
{
    public class Variable
    {
        public string Name { get; set; }

        public VariableType Type { get; set; }

        public string Value { get; set; }
    }
}
