using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HARIA.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ambients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ambients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    LastActivity = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExternalSensors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<int>(type: "INTEGER", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    Condition = table.Column<string>(type: "TEXT", nullable: true),
                    Script = table.Column<string>(type: "TEXT", nullable: true),
                    LastStateChange = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalSensors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    IsError = table.Column<bool>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Data = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scenarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    Icon = table.Column<string>(type: "TEXT", nullable: true),
                    Color = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scenarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ScenarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    StaticState = table.Column<bool>(type: "INTEGER", nullable: false),
                    ActuatorMessage = table.Column<string>(type: "TEXT", nullable: true),
                    Script = table.Column<string>(type: "TEXT", nullable: true),
                    Invert = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actions_Scenarios_ScenarioId",
                        column: x => x.ScenarioId,
                        principalTable: "Scenarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScenarioTriggers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    InitialTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FinalTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExternalId = table.Column<int>(type: "INTEGER", nullable: true),
                    ScenarioId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScenarioTriggers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScenarioTriggers_ExternalSensors_ExternalId",
                        column: x => x.ExternalId,
                        principalTable: "ExternalSensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScenarioTriggers_Scenarios_ScenarioId",
                        column: x => x.ScenarioId,
                        principalTable: "Scenarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActionExternalSensor",
                columns: table => new
                {
                    ActionsId = table.Column<int>(type: "INTEGER", nullable: false),
                    ExternalId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionExternalSensor", x => new { x.ActionsId, x.ExternalId });
                    table.ForeignKey(
                        name: "FK_ActionExternalSensor_Actions_ActionsId",
                        column: x => x.ActionsId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionExternalSensor_ExternalSensors_ExternalId",
                        column: x => x.ExternalId,
                        principalTable: "ExternalSensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionPeriods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ActionId = table.Column<int>(type: "INTEGER", nullable: false),
                    InitialTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FinalTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionPeriods_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Actuators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceId = table.Column<int>(type: "INTEGER", nullable: false),
                    AmbientId = table.Column<int>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockState = table.Column<bool>(type: "INTEGER", nullable: false),
                    DefaultActiveTime = table.Column<int>(type: "INTEGER", nullable: false),
                    DeactivationTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastStateChange = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ActionId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actuators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actuators_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Actuators_Ambients_AmbientId",
                        column: x => x.AmbientId,
                        principalTable: "Ambients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Actuators_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceId = table.Column<int>(type: "INTEGER", nullable: false),
                    AmbientId = table.Column<int>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<int>(type: "INTEGER", nullable: false),
                    ActiveLowerBound = table.Column<int>(type: "INTEGER", nullable: false),
                    ActiveUpperBound = table.Column<int>(type: "INTEGER", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    LastStateChange = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ActionId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensors_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sensors_Ambients_AmbientId",
                        column: x => x.AmbientId,
                        principalTable: "Ambients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sensors_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "PasswordHash" },
                values: new object[] { 1, "admin", "21232f297a57a5a743894a0e4a801fc3" });

            migrationBuilder.CreateIndex(
                name: "IX_ActionExternalSensor_ExternalId",
                table: "ActionExternalSensor",
                column: "ExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionPeriods_ActionId",
                table: "ActionPeriods",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_ScenarioId",
                table: "Actions",
                column: "ScenarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Actuators_ActionId",
                table: "Actuators",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_Actuators_AmbientId",
                table: "Actuators",
                column: "AmbientId");

            migrationBuilder.CreateIndex(
                name: "IX_Actuators_DeviceId",
                table: "Actuators",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_ScenarioTriggers_ExternalId",
                table: "ScenarioTriggers",
                column: "ExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_ScenarioTriggers_ScenarioId",
                table: "ScenarioTriggers",
                column: "ScenarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_ActionId",
                table: "Sensors",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_AmbientId",
                table: "Sensors",
                column: "AmbientId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_DeviceId",
                table: "Sensors",
                column: "DeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionExternalSensor");

            migrationBuilder.DropTable(
                name: "ActionPeriods");

            migrationBuilder.DropTable(
                name: "Actuators");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "ScenarioTriggers");

            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ExternalSensors");

            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropTable(
                name: "Ambients");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Scenarios");
        }
    }
}
