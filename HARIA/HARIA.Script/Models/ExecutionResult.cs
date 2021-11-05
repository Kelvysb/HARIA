using HARIA.Script.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARIA.Script.Models
{
    public class ExecutionResult
    {
        public ResultCode ResultCode { get; set; }

        public List<string> Results { get; set; }
    }
}
