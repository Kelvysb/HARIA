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

        protected Context()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={config.GetSection("DataBase")["Server"]}", b => b.MigrationsAssembly("HARIA.API"));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .ToTable("Devices")
                .HasKey(t => t.Id);

            modelBuilder.Entity<Device>()
                .HasMany(t => t.Sensors);

            modelBuilder.Entity<Device>()
                .HasMany(t => t.Actuators);

            modelBuilder.Entity<Sensor>()
                .ToTable("Sensors")
                .HasKey(t => t.Id);

            modelBuilder.Entity<Actuator>()
                .ToTable("Actuators")
                .HasKey(t => t.Id);

            modelBuilder.Entity<Ambient>()
                .ToTable("Ambients")
                .HasKey(t => t.Id);

            modelBuilder.Entity<Ambient>()
                .HasMany(t => t.Actuators);

            modelBuilder.Entity<Ambient>()
                .HasMany(t => t.Sensors);

            modelBuilder.Entity<ExternalSensor>()
                .ToTable("ExternalSensors")
                .HasKey(t => t.Id);

            modelBuilder.Entity<ExternalSensor>()
                .ToTable("ExternalSensors")
                .HasKey(t => t.Id);

            modelBuilder.Entity<Action>()
                .ToTable("Actions")
                .HasKey(t => t.Id);

            modelBuilder.Entity<Action>()
                .HasMany(t => t.Sensors)
                .WithMany(t => t.Actions);

            modelBuilder.Entity<Action>()
                .HasMany(t => t.Actuators)
                .WithMany(t => t.Actions);

            modelBuilder.Entity<Action>()
                .HasOne(t => t.External);

            modelBuilder.Entity<Action>()
                .HasMany(t => t.ActionPeriods);

            modelBuilder.Entity<ActionPeriod>()
                .ToTable("ActionPeriods")
                .HasKey(t => t.Id);

            modelBuilder.Entity<Scenario>()
                .ToTable("Scenarios")
                .HasKey(t => t.Id);

            modelBuilder.Entity<Scenario>()
                .HasMany(t => t.Actions);

            modelBuilder.Entity<Scenario>()
                .HasMany(t => t.Triggers);

            modelBuilder.Entity<ScenarioTrigger>()
                .ToTable("ScenarioTriggers")
                .HasKey(t => t.Id);

            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasKey(t => t.Id);

            modelBuilder.Entity<Log>()
                .ToTable("Logs")
                .HasKey(t => t.Id);

            modelBuilder.Entity<User>()
                .HasData(new User() { Id = 1, Name = "admin", PasswordHash = AuthHelper.GetMd5Hash("admin") });

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