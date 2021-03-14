using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HARIA.Domain.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace HARIA.Domain.Helpers
{
    public static class AuthHelper
    {
        public static string GetToken(User user, string secret, string issuer)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), issuer),
                    new Claim(ClaimTypes.Name, user.Name, issuer),
                    new Claim(ClaimTypes.Role, string.Join(",", user.Roles.Select(r => r.Name)), issuer),
                    new Claim(Constants.ClaimsNames.PERMISSIONS, string.Join(",", user.Roles.SelectMany(r => r.Permissions.Select(p => p.Code))), issuer)
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = issuer
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }

        public static void CheckPermissions(ClaimsPrincipal principal, params string[] permissions)
        {
            if (principal is null || permissions is null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            var userPermissions = principal.Claims.First(claim => claim.Type == Constants.ClaimsNames.PERMISSIONS).Value.Split(",");

            if (!userPermissions.Any(p => permissions.Contains(p)))
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}