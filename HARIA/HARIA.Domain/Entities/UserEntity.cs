using System.Collections.Generic;
using HARIA.Domain.Attributes;

namespace HARIA.Domain.Entities
{
    [Collection("Users")]
    public class UserEntity : EntityBase
    {
        public string Password { get; set; }

        public List<string> Permissions { get; set; }
    }
}