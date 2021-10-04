using System.Collections.Generic;

namespace HARIA.Domain.Models
{
    public class User
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public List<string> Permissions { get; set; }

        public string Token { get; set; }

        public string NewPassword { get; set; }
    }
}