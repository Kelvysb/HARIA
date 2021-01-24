using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Entities;
using HARIA.Domain.Helpers.HARIA.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HARIA.DataAccess
{
    public class Context : DbContext, IContext
    {
        private readonly IConfiguration config;

        public Context(IConfiguration config)
        {
            this.config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={config.GetSection("DataBase")["Server"]}", b => b.MigrationsAssembly("HARIA.API"));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeviceEntity>()
                .ToTable("Devices")
                .HasKey(t => t.Id);

            modelBuilder.Entity<DeviceEntity>()
                .HasMany(t => t.Sensors);

            modelBuilder.Entity<DeviceEntity>()
                .HasMany(t => t.Actuators);

            modelBuilder.Entity<SensorEntity>()
                .ToTable("Sensors")
                .HasKey(t => t.Id);

            modelBuilder.Entity<ActuatorEntity>()
                .ToTable("Actuators")
                .HasKey(t => t.Id);

            modelBuilder.Entity<AmbientEntity>()
                .ToTable("Ambients")
                .HasKey(t => t.Id);

            modelBuilder.Entity<AmbientEntity>()
                .HasMany(t => t.Actuators);

            modelBuilder.Entity<AmbientEntity>()
                .HasMany(t => t.Sensors);

            modelBuilder.Entity<ExternalSensorEntity>()
                .ToTable("ExternalSensors")
                .HasKey(t => t.Id);

            modelBuilder.Entity<ExternalActuatorEntity>()
                .ToTable("ExternalActuators")
                .HasKey(t => t.Id);

            modelBuilder.Entity<ActionEventEntity>()
                .ToTable("ActionEvents")
                .HasKey(t => t.Id);

            modelBuilder.Entity<ActionEventEntity>()
                .HasMany(t => t.Sensors)
                .WithMany(t => t.Actions)
                .UsingEntity(r => r.ToTable("ActionEventsSensors"));

            modelBuilder.Entity<ActionEventEntity>()
                .HasMany(t => t.Actuators)
                .WithMany(t => t.Actions)
                .UsingEntity(r => r.ToTable("ActionEventsActuators"));

            modelBuilder.Entity<ActionEventEntity>()
                .HasMany(t => t.ExternalSensors)
                .WithMany(t => t.Actions)
                .UsingEntity(r => r.ToTable("ActionEventsExternalSensors"));

            modelBuilder.Entity<ActionEventEntity>()
                .HasMany(t => t.ExternalActuators)
                .WithMany(t => t.Actions)
                .UsingEntity(r => r.ToTable("ActionEventsExternalActuators"));

            modelBuilder.Entity<ActionEventEntity>()
                .HasMany(t => t.ActionPeriods);

            modelBuilder.Entity<ActionEventPeriodEntity>()
                .ToTable("ActionEventPeriods")
                .HasKey(t => t.Id);

            modelBuilder.Entity<ScenarioEntity>()
                .ToTable("Scenarios")
                .HasKey(t => t.Id);

            modelBuilder.Entity<ScenarioEntity>()
                .HasMany(t => t.Actions)
                .WithMany(t => t.Scenarios)
                .UsingEntity(r => r.ToTable("ScenarioActionEvents"));

            modelBuilder.Entity<ScenarioEntity>()
                .HasMany(t => t.Triggers);

            modelBuilder.Entity<ScenarioTriggerEntity>()
                .ToTable("ScenarioTriggers")
                .HasKey(t => t.Id);

            modelBuilder.Entity<ScenarioTriggerEntity>()
                .HasMany(t => t.Sensors)
                .WithMany(t => t.ScenarioTriggers)
                .UsingEntity(r => r.ToTable("ScenarioTriggersSensors"));

            modelBuilder.Entity<ScenarioTriggerEntity>()
                .HasMany(t => t.ExternalSensors)
                .WithMany(t => t.ScenarioTriggers)
                .UsingEntity(r => r.ToTable("ScenarioTriggersExternalSensors"));

            modelBuilder.Entity<UserEntity>()
                .ToTable("Users")
                .HasKey(t => t.Id);

            modelBuilder.Entity<LogEntity>()
                .ToTable("Logs")
                .HasKey(t => t.Id);

            modelBuilder.Entity<UserEntity>()
                .HasData(new UserEntity() { Id = 1, Name = "admin", PasswordHash = AuthHelper.GetMd5Hash("admin") });

            base.OnModelCreating(modelBuilder);
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public DbSet<T> GetSet<T>()
            where T : class
        {
            return Set<T>();
        }
    }
}