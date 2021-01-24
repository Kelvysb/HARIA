using HARIA.DataAccess;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace HARIA.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HARIA.API", Version = "v1" });
            });

            services.AddTransient<IContext, Context>();
            services.AddTransient<IActionsRepository, ActionsRepository>();
            services.AddTransient<IActionPeriodsRepository, ActionPeriodsRepository>();
            services.AddTransient<IActuatorsRepository, ActuatorsRepository>();
            services.AddTransient<IAmbientsRepository, AmbientsRepository>();
            services.AddTransient<IDevicesRepository, DevicesRepository>();
            services.AddTransient<IExternalSensorsRepository, ExternalSensorsRepository>();
            services.AddTransient<ILogsRepository, LogsRepository>();
            services.AddTransient<IScenariosRepository, ScenariosRepository>();
            services.AddTransient<IScenarioTriggersRepository, ScenarioTriggersRepository>();
            services.AddTransient<ISensorsRepository, SensorsRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();

            services.AddTransient<IActionsService, ActionsService>();
            services.AddTransient<IActionPeriodsService, ActionPeriodsService>();
            services.AddTransient<IActuatorsService, ActuatorsService>();
            services.AddTransient<IAmbientsService, AmbientsService>();
            services.AddTransient<IDevicesService, DevicesService>();
            services.AddTransient<IExternalSensorsService, ExternalSensorsService>();
            services.AddTransient<ILogsService, LogsService>();
            services.AddTransient<IScenariosService, ScenariosService>();
            services.AddTransient<IScenarioTriggersService, ScenarioTriggersService>();
            services.AddTransient<ISensorsService, SensorsService>();
            services.AddTransient<IUsersService, UsersService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HARIA.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (Context context = new Context(Configuration))
            {
                context.Database.EnsureCreated();
            }
        }
    }
}