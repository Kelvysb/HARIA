using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HARIA.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    StaticState = table.Column<bool>(type: "INTEGER", nullable: false),
                    ActuatorMessage = table.Column<string>(type: "TEXT", nullable: true),
                    Invert = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionEvents", x => x.Id);
                });

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
                name: "ExternalActuators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Script = table.Column<string>(type: "TEXT", nullable: true),
                    LastExecution = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalActuators", x => x.Id);
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
                name: "ActionEventPeriods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ActionId = table.Column<int>(type: "INTEGER", nullable: false),
                    InitialTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FinalTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ActionEventEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionEventPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionEventPeriods_ActionEvents_ActionEventEntityId",
                        column: x => x.ActionEventEntityId,
                        principalTable: "ActionEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    AmbientEntityId = table.Column<int>(type: "INTEGER", nullable: true),
                    DeviceEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actuators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actuators_Ambients_AmbientEntityId",
                        column: x => x.AmbientEntityId,
                        principalTable: "Ambients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Actuators_Devices_DeviceEntityId",
                        column: x => x.DeviceEntityId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    AmbientEntityId = table.Column<int>(type: "INTEGER", nullable: true),
                    DeviceEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensors_Ambients_AmbientEntityId",
                        column: x => x.AmbientEntityId,
                        principalTable: "Ambients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sensors_Devices_DeviceEntityId",
                        column: x => x.DeviceEntityId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActionEventsExternalActuators",
                columns: table => new
                {
                    ActionsId = table.Column<int>(type: "INTEGER", nullable: false),
                    ExternalActuatorsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionEventsExternalActuators", x => new { x.ActionsId, x.ExternalActuatorsId });
                    table.ForeignKey(
                        name: "FK_ActionEventsExternalActuators_ActionEvents_ActionsId",
                        column: x => x.ActionsId,
                        principalTable: "ActionEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionEventsExternalActuators_ExternalActuators_ExternalActuatorsId",
                        column: x => x.ExternalActuatorsId,
                        principalTable: "ExternalActuators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionEventsExternalSensors",
                columns: table => new
                {
                    ActionsId = table.Column<int>(type: "INTEGER", nullable: false),
                    ExternalSensorsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionEventsExternalSensors", x => new { x.ActionsId, x.ExternalSensorsId });
                    table.ForeignKey(
                        name: "FK_ActionEventsExternalSensors_ActionEvents_ActionsId",
                        column: x => x.ActionsId,
                        principalTable: "ActionEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionEventsExternalSensors_ExternalSensors_ExternalSensorsId",
                        column: x => x.ExternalSensorsId,
                        principalTable: "ExternalSensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScenarioActionEvents",
                columns: table => new
                {
                    ActionsId = table.Column<int>(type: "INTEGER", nullable: false),
                    ScenariosId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScenarioActionEvents", x => new { x.ActionsId, x.ScenariosId });
                    table.ForeignKey(
                        name: "FK_ScenarioActionEvents_ActionEvents_ActionsId",
                        column: x => x.ActionsId,
                        principalTable: "ActionEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScenarioActionEvents_Scenarios_ScenariosId",
                        column: x => x.ScenariosId,
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
                    ScenarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    InitialTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FinalTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ScenarioEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScenarioTriggers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScenarioTriggers_Scenarios_ScenarioEntityId",
                        column: x => x.ScenarioEntityId,
                        principalTable: "Scenarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActionEventsActuators",
                columns: table => new
                {
                    ActionsId = table.Column<int>(type: "INTEGER", nullable: false),
                    ActuatorsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionEventsActuators", x => new { x.ActionsId, x.ActuatorsId });
                    table.ForeignKey(
                        name: "FK_ActionEventsActuators_ActionEvents_ActionsId",
                        column: x => x.ActionsId,
                        principalTable: "ActionEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionEventsActuators_Actuators_ActuatorsId",
                        column: x => x.ActuatorsId,
                        principalTable: "Actuators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionEventsSensors",
                columns: table => new
                {
                    ActionsId = table.Column<int>(type: "INTEGER", nullable: false),
                    SensorsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionEventsSensors", x => new { x.ActionsId, x.SensorsId });
                    table.ForeignKey(
                        name: "FK_ActionEventsSensors_ActionEvents_ActionsId",
                        column: x => x.ActionsId,
                        principalTable: "ActionEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionEventsSensors_Sensors_SensorsId",
                        column: x => x.SensorsId,
                        principalTable: "Sensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScenarioTriggersExternalSensors",
                columns: table => new
                {
                    ExternalSensorsId = table.Column<int>(type: "INTEGER", nullable: false),
                    ScenarioTriggersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScenarioTriggersExternalSensors", x => new { x.ExternalSensorsId, x.ScenarioTriggersId });
                    table.ForeignKey(
                        name: "FK_ScenarioTriggersExternalSensors_ExternalSensors_ExternalSensorsId",
                        column: x => x.ExternalSensorsId,
                        principalTable: "ExternalSensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScenarioTriggersExternalSensors_ScenarioTriggers_ScenarioTriggersId",
                        column: x => x.ScenarioTriggersId,
                        principalTable: "ScenarioTriggers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScenarioTriggersSensors",
                columns: table => new
                {
                    ScenarioTriggersId = table.Column<int>(type: "INTEGER", nullable: false),
                    SensorsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScenarioTriggersSensors", x => new { x.ScenarioTriggersId, x.SensorsId });
                    table.ForeignKey(
                        name: "FK_ScenarioTriggersSensors_ScenarioTriggers_ScenarioTriggersId",
                        column: x => x.ScenarioTriggersId,
                        principalTable: "ScenarioTriggers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScenarioTriggersSensors_Sensors_SensorsId",
                        column: x => x.SensorsId,
                        principalTable: "Sensors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "PasswordHash" },
                values: new object[] { 1, "admin", "21232f297a57a5a743894a0e4a801fc3" });

            migrationBuilder.CreateIndex(
                name: "IX_ActionEventPeriods_ActionEventEntityId",
                table: "ActionEventPeriods",
                column: "ActionEventEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionEventsActuators_ActuatorsId",
                table: "ActionEventsActuators",
                column: "ActuatorsId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionEventsExternalActuators_ExternalActuatorsId",
                table: "ActionEventsExternalActuators",
                column: "ExternalActuatorsId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionEventsExternalSensors_ExternalSensorsId",
                table: "ActionEventsExternalSensors",
                column: "ExternalSensorsId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionEventsSensors_SensorsId",
                table: "ActionEventsSensors",
                column: "SensorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Actuators_AmbientEntityId",
                table: "Actuators",
                column: "AmbientEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Actuators_DeviceEntityId",
                table: "Actuators",
                column: "DeviceEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ScenarioActionEvents_ScenariosId",
                table: "ScenarioActionEvents",
                column: "ScenariosId");

            migrationBuilder.CreateIndex(
                name: "IX_ScenarioTriggers_ScenarioEntityId",
                table: "ScenarioTriggers",
                column: "ScenarioEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ScenarioTriggersExternalSensors_ScenarioTriggersId",
                table: "ScenarioTriggersExternalSensors",
                column: "ScenarioTriggersId");

            migrationBuilder.CreateIndex(
                name: "IX_ScenarioTriggersSensors_SensorsId",
                table: "ScenarioTriggersSensors",
                column: "SensorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_AmbientEntityId",
                table: "Sensors",
                column: "AmbientEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_DeviceEntityId",
                table: "Sensors",
                column: "DeviceEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionEventPeriods");

            migrationBuilder.DropTable(
                name: "ActionEventsActuators");

            migrationBuilder.DropTable(
                name: "ActionEventsExternalActuators");

            migrationBuilder.DropTable(
                name: "ActionEventsExternalSensors");

            migrationBuilder.DropTable(
                name: "ActionEventsSensors");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "ScenarioActionEvents");

            migrationBuilder.DropTable(
                name: "ScenarioTriggersExternalSensors");

            migrationBuilder.DropTable(
                name: "ScenarioTriggersSensors");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Actuators");

            migrationBuilder.DropTable(
                name: "ExternalActuators");

            migrationBuilder.DropTable(
                name: "ActionEvents");

            migrationBuilder.DropTable(
                name: "ExternalSensors");

            migrationBuilder.DropTable(
                name: "ScenarioTriggers");

            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropTable(
                name: "Scenarios");

            migrationBuilder.DropTable(
                name: "Ambients");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
