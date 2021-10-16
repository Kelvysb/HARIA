using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARIA.Domain.Entities
{
    public class Schedule: EntityBase    
    {
        public DateTime CreationTime { get; set; }
        public DateTime ExecTime { get; set; }
        public string Command { get; set; }
    }
}
