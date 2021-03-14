using System.Text;
using AutoMapper;
using HARIA.DataAccess;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Mappers;
using HARIA.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace HARIA.API
{
    public class Startup
    {
        private const string CORS_POLICY = "AllowedOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CORS_POLICY,
                                  builder =>
                                  {
                                      builder
                                      .AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HARIA.API", Version = "v1" });
                c.AddSecurityDefinition("JWT Token", new OpenApiSecurityScheme
                {
                    Description = "JWT Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header
                });
            });

            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("Auth").GetValue<string>("Secret"));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddTransient<IContext, Context>();
            services.AddTransient<IActionEventsRepository, ActionEventsRepository>();
            services.AddTransient<IActionEventPeriodsRepository, ActionEventPeriodsRepository>();
            services.AddTransient<IActuatorsRepository, ActuatorsRepository>();
            services.AddTransient<IAmbientsRepository, AmbientsRepository>();
            services.AddTransient<INodesRepository, NodesRepository>();
            services.AddTransient<IExternalSensorsRepository, ExternalSensorsRepository>();
            services.AddTransient<IExternalActuatorsRepository, ExternalActuatorsRepository>();
            services.AddTransient<IScenariosRepository, ScenariosRepository>();
            services.AddTransient<IScenarioTriggersRepository, ScenarioTriggersRepository>();
            services.AddTransient<ISensorsRepository, SensorsRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IRolesRepository, RolesRepository>();
            services.AddTransient<IStatesRepository, StatesRepository>();

            services.AddTransient<IActionEventsService, ActionEventsService>();
            services.AddTransient<IActionEventPeriodsService, ActionEventPeriodsService>();
            services.AddTransient<IActuatorsService, ActuatorsService>();
            services.AddTransient<IAmbientsService, AmbientsService>();
            services.AddTransient<INodesService, NodesService>();
            services.AddTransient<IExternalSensorsService, ExternalSensorsService>();
            services.AddTransient<IExternalActuatorsService, ExternalActuatorsService>();
            services.AddTransient<IScenariosService, ScenariosService>();
            services.AddTransient<IScenarioTriggersService, ScenarioTriggersService>();
            services.AddTransient<ISensorsService, SensorsService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IRolesService, RolesService>();
            services.AddTransient<IStatesService, StatesService>();
            services.AddTransient<IEngineService, EngineService>();
            services.AddTransient<IPythonEngine, PythonEngine>();

            services.AddAutoMapper(typeof(MapperConfig));
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

            app.UseCors(CORS_POLICY);

            app.UseAuthentication();

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