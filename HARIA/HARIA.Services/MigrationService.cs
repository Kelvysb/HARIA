using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Data;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Entities;
using HARIA.Domain.Helpers;
using Microsoft.Extensions.Configuration;

namespace HARIA.Services
{
    public class MigrationService : IMigrationService
    {
        private IConfiguration configuration;
        private IUsersRepository usersRepository;

        public MigrationService(
            IConfiguration configuration,
            IUsersRepository usersRepository)
        {
            this.configuration = configuration;
            this.usersRepository = usersRepository;
        }

        public async Task Migrate()
        {
            await Users();
        }

        private async Task Users()
        {
            var defaultUsers = new List<UserEntity>
            {
                new UserEntity
                {
                    Id = "admin",
                    Password = AuthHelper.GetMd5Hash(configuration["ADMIN_DEFAULT_PASSWORD"]),
                    Permissions = new List<string> { "ADMIN", "DASHBOARD", "SERVICE", "KIOSK" }
                }
            };

            var users = await usersRepository.GetAll();

            var newUsers = defaultUsers.Where(p => !users.Any(a => a.Id == p.Id)).ToList();

            if (newUsers.Any())
            {
                await usersRepository.BulkUpsert(newUsers);
            }
        }
    }
}