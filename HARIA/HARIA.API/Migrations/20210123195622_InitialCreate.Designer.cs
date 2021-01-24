﻿// <auto-generated />
using System;
using HARIA.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HARIA.API.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20210123195622_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("ActionExternalSensor", b =>
                {
                    b.Property<int>("ActionsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExternalId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ActionsId", "ExternalId");

                    b.HasIndex("ExternalId");

                    b.ToTable("ActionExternalSensor");
                });

            modelBuilder.Entity("HARIA.Domain.Entities.Action", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ActuatorMessage")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Invert")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ScenarioId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Script")
                        .HasColumnType("TEXT");

                    b.Property<bool>("StaticState")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ScenarioId");

                    b.ToTable("Actions");
                });

            modelBuilder.Entity("HARIA.Domain.Entities.ActionPeriod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ActionId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FinalTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("InitialTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ActionId");

                    b.ToTable("ActionPeriods");
                });

            modelBuilder.Entity("HARIA.Domain.Entities.Actuator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ActionId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AmbientId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DeactivationTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("DefaultActiveTime")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("DeviceId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastStateChange")
                        .HasColumnType("TEXT");

                    b.Property<bool>("LockState")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ActionId");

                    b.HasIndex("AmbientId");

                    b.HasIndex("DeviceId");

                    b.ToTable("Actuators");
                });

            modelBuilder.Entity("HARIA.Domain.Entities.Ambient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Ambients");
                });

            modelBuilder.Entity("HARIA.Domain.Entities.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastActivity")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("HARIA.Domain.Entities.ExternalSensor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Condition")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastStateChange")
                        .HasColumnType("TEXT");

                    b.Property<string>("Script")
                        .HasColumnType("TEXT");

                    b.Property<int>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("ExternalSensors");
                });

            modelBuilder.Entity("HARIA.Domain.Entities.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Data")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsError")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Time")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("HARIA.Domain.Entities.Scenario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Color")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Icon")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Priority")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Scenarios");
                });

            modelBuilder.Entity("HARIA.Domain.Entities.ScenarioTrigger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ExternalId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FinalTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("InitialTime")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ScenarioId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ExternalId");

                    b.HasIndex("ScenarioId");

                    b.ToTable("ScenarioTriggers");
                });

            modelBuilder.Entity("HARIA.Domain.Entities.Sensor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ActionId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ActiveLowerBound")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ActiveUpperBound")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AmbientId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("DeviceId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastStateChange")
                        .HasColumnType("TEXT");

                    b.Property<int>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ActionId");

                    b.HasIndex("AmbientId");

                    b.HasIndex("DeviceId");

                    b.ToTable("Sensors");
                });

            modelBuilder.Entity("HARIA.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "admin",
                            PasswordHash = "21232f297a57a5a743894a0e4a801fc3"
                        });
                });

            modelBuilder.Entity("ActionExternalSensor", b =>
                {
                    b.HasOne("HARIA.Domain.Entities.Action", null)
                        .WithMany()
                        .HasForeignKey("ActionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HARIA.Domain.Entities.ExternalSensor", null)
                        .WithMany()
                        .HasForeignKey("ExternalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HARIA.Domain.Entities.Action", b =>
                {
                    b.HasOne("HARIA.Domain.Entities.Scenario", null)
                        .WithMany("Actions")
                        .HasForeignKey("ScenarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HARIA.Domain.Entities.ActionPeriod", b =>
                {
                    b.HasOne("HARIA.Domain.Entities.Action", null)
                        .WithMany("ActionPeriods")
                        .HasForeignKey("ActionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HARIA.Domain.Entities.Actuator", b =>
                {
                    b.HasOne("HARIA.Domain.Entities.Action", null)
                        .WithMany("Actuators")
                        .HasForeignKey("ActionId");

                    b.HasOne("HARIA.Domain.Entities.Ambient", null)
                        .WithMany("Actuators")
                        .HasForeignKey("AmbientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HARIA.Domain.Entities.Device", null)
                        .WithMany("Actuators")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HARIA.Domain.Entities.ScenarioTrigger", b =>
                {
                    b.HasOne("HARIA.Domain.Entities.ExternalSensor", "External")
                        .WithMany()
                        .HasForeignKey("ExternalId");

                    b.HasOne("HARIA.Domain.Entities.Scenario", null)
                        .WithMany("Triggers")
                        .HasForeignKey("ScenarioId");

                    b.Navigation("External");
                });

            modelBuilder.Entity("HARIA.Domain.Entities.Sensor", b =>
                {
                    b.HasOne("HARIA.Domain.Entities.Action", null)
                        .WithMany("Sensors")
                        .HasForeignKey("ActionId");

                    b.HasOne("HARIA.Domain.Entities.Ambient", null)
                        .WithMany("Sensors")
                        .HasForeignKey("AmbientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HARIA.Domain.Entities.Device", null)
                        .WithMany("Sensors")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HARIA.Domain.Entities.Action", b =>
                {
                    b.Navigation("ActionPeriods");

                    b.Navigation("Actuators");

                    b.Navigation("Sensors");
                });

            modelBuilder.Entity("HARIA.Domain.Entities.Ambient", b =>
                {
                    b.Navigation("Actuators");

                    b.Navigation("Sensors");
                });

            modelBuilder.Entity("HARIA.Domain.Entities.Device", b =>
                {
                    b.Navigation("Actuators");

                    b.Navigation("Sensors");
                });

            modelBuilder.Entity("HARIA.Domain.Entities.Scenario", b =>
                {
                    b.Navigation("Actions");

                    b.Navigation("Triggers");
                });
#pragma warning restore 612, 618
        }
    }
}
