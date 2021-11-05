using HARIA.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HARIA.Domain.Entities
{
    [Collection("Blackboard")]
    public class BlackboardEntity : EntityBase
    {
        public string Value { get; set; }
    }
}
