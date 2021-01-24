using HARIA.Domain.Abstractions.DTOs;

namespace HARIA.Domain.DTOs
{
    public class User : IDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public string NewPasswordHash { get; set; }

        public string Token { get; set; }
    }
}