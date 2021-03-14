using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Repositories;
using HARIA.Domain.Constants;
using HARIA.Domain.Entities;
using HARIA.Domain.Helpers;
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
            optionsBuilder.LogTo(Console.WriteLine);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NodeEntity>()
                .ToTable("Nodes")
                .HasKey(t => t.Id);

            modelBuilder.Entity<NodeEntity>()
                .HasMany(t => t.Sensors)
                .WithOne(s => s.Node)
                .HasForeignKey(s => s.NodeId);

            modelBuilder.Entity<NodeEntity>()
                .HasMany(t => t.Actuators)
                .WithOne(a => a.Node)
                .HasForeignKey(a => a.NodeId);

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
                .HasMany(t => t.Actuators)
                .WithOne(a => a.Ambient)
                .HasForeignKey(a => a.AmbientId);

            modelBuilder.Entity<AmbientEntity>()
                .HasMany(t => t.Sensors)
                .WithOne(s => s.Ambient)
                .HasForeignKey(s => s.AmbientId);

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
                .HasMany(t => t.ActionPeriods)
                .WithOne(p => p.ActionEvent)
                .HasForeignKey(p => p.ActionEventId);

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
                .HasMany(t => t.Triggers)
                .WithOne(t => t.Scenario)
                .HasForeignKey(t => t.ScenarioId);

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

            modelBuilder.Entity<UserEntity>()
                .HasMany(t => t.Roles)
                .WithMany(t => t.Users)
                .UsingEntity<Dictionary<string, object>>(
                        "UsersRoles",
                        x => x.HasOne<RoleEntity>()
                            .WithMany()
                            .OnDelete(DeleteBehavior.Cascade),
                        x => x.HasOne<UserEntity>()
                            .WithMany()
                            .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<RoleEntity>()
                .ToTable("Roles")
                .HasKey(t => t.Id);

            modelBuilder.Entity<RoleEntity>()
                .HasMany(t => t.Permissions)
                .WithMany(t => t.Roles)
                .UsingEntity<Dictionary<string, object>>(
                        "RolesPermissions",
                        x => x.HasOne<PermissionEntity>()
                            .WithMany()
                            .OnDelete(DeleteBehavior.Cascade),
                        x => x.HasOne<RoleEntity>()
                            .WithMany()
                            .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<PermissionEntity>()
                .ToTable("Permissions")
                .HasKey(t => t.Id);

            modelBuilder.Entity<StateEntity>()
                .ToTable("States")
                .HasKey(t => t.Id);

            modelBuilder.Entity<PermissionEntity>()
                .HasData(
                    new PermissionEntity()
                    {
                        Id = 1,
                        Code = Permissions.SERVICE,
                        Description = "Access device service endpoints"
                    },
                    new PermissionEntity()
                    {
                        Id = 2,
                        Code = Permissions.DASHBOARD,
                        Description = "View Dashboard"
                    },
                    new PermissionEntity()
                    {
                        Id = 3,
                        Code = Permissions.CONFIGURE,
                        Description = "Configure system"
                    },
                    new PermissionEntity()
                    {
                        Id = 4,
                        Code = Permissions.KIOSK,
                        Description = "Kiosk mode"
                    });

            modelBuilder.Entity<RoleEntity>()
                .HasData(
                    new RoleEntity()
                    {
                        Id = 1,
                        Name = "Admin",
                        Description = "System Administrator"
                    },
                    new RoleEntity()
                    {
                        Id = 2,
                        Name = "Device",
                        Description = "Device"
                    },
                    new RoleEntity()
                    {
                        Id = 3,
                        Name = "Worker",
                        Description = "Worker"
                    },
                    new RoleEntity()
                    {
                        Id = 4,
                        Name = "Kiosk",
                        Description = "Kiosk mode"
                    });

            modelBuilder.Entity<UserEntity>()
                .HasData(
                    new UserEntity()
                    {
                        Id = 1,
                        Name = "admin",
                        PasswordHash = AuthHelper.GetMd5Hash("admin")
                    },
                    new UserEntity()
                    {
                        Id = 2,
                        Name = "device",
                        PasswordHash = AuthHelper.GetMd5Hash("admin")
                    },
                    new UserEntity()
                    {
                        Id = 3,
                        Name = "worker",
                        PasswordHash = AuthHelper.GetMd5Hash("admin")
                    },
                    new UserEntity()
                    {
                        Id = 4,
                        Name = "kiosk",
                        PasswordHash = AuthHelper.GetMd5Hash("admin")
                    });

            modelBuilder.Entity("RolesPermissions")
                .HasData(
                new { RolesId = 1, PermissionsId = 1 },
                new { RolesId = 1, PermissionsId = 2 },
                new { RolesId = 1, PermissionsId = 3 },
                new { RolesId = 2, PermissionsId = 1 },
                new { RolesId = 3, PermissionsId = 1 },
                new { RolesId = 4, PermissionsId = 2 },
                new { RolesId = 4, PermissionsId = 4 }
                );

            modelBuilder.Entity("UsersRoles")
                .HasData(
                new { UsersId = 1, RolesId = 1 },
                new { UsersId = 2, RolesId = 2 },
                new { UsersId = 3, RolesId = 3 },
                new { UsersId = 4, RolesId = 4 }
                );

            modelBuilder.Entity<StateEntity>()
                .HasData(
                new StateEntity()
                {
                    Id = 1,
                    Key = "ACTIVE_SCENARIO",
                    Value = "1",
                    DefaultValue = "1",
                    IsSystemDefault = true
                },
                new StateEntity()
                {
                    Id = 2,
                    Key = "SCENARIO_MODE",
                    Value = "AUTO",
                    DefaultValue = "AUTO",
                    IsSystemDefault = true
                });

            modelBuilder.Entity<ScenarioEntity>()
                .HasData(
                new ScenarioEntity()
                {
                    Id = 1,
                    Name = "Default",
                    Description = "Default scenario",
                    Color = "#F2F2F2",
                    Icon = "default.svg",
                    Priority = int.MaxValue,
                    IsDefault = true
                });

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

        public void Atach<T>(T entity) where T : class
        {
            base.Attach(entity);
        }

        public void Deatach<T>(T entity) where T : class
        {
            base.Entry(entity).State = EntityState.Detached;
        }
    }
}