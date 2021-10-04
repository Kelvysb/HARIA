using HARIA.Domain.Abstractions.Services;
using Microsoft.AspNetCore.Builder;

namespace HARIA.Domain.Extensions
{
    public static class AppExtenssions
    {
        public static IApplicationBuilder MigrateHariaDb(this IApplicationBuilder app)
        {
            var migration = (IMigrationService)app.ApplicationServices.GetService(typeof(IMigrationService));
            migration.Migrate();
            return app;
        }
    }
}