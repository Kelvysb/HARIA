using FluentAssertions;
using HARIA.Domain.Entities;
using HARIA.Script.Abstractions;
using HARIA.Script.Enums;
using HARIA.Script.Interpreter;
using HARIA.Script.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace HARIA.Script.Tests
{
    [TestClass]
    public class InterpreterTests
    {
        private readonly HSInterpreter interpreter;
        private readonly Mock<IExecutor> executor;
        private List<DeviceDataEntity> devices = new();
        private List<Schedule> schedules = new();
        private List<BlackboardEntity> blackboard = new();

        public InterpreterTests()
        {
            executor = new Mock<IExecutor>();
            MockSource();
            interpreter = new HSInterpreter(executor.Object);
        }

        [TestMethod]
        public void SimpleGet()
        {
            string command = "GET: Sensor.Hall;";

            var result = interpreter.Execute(command).Result;

            result.Should().NotBeNull();
            result.ResultCode.Should().Be(Enums.ResultCode.Succsses);
            result.Results.First().Should().Be("False");

            executor.Verify(exec => exec.Get(
                    It.Is<SourceGroup>(g => g == SourceGroup.Device),
                    It.Is<string>(s => s.Equals("SA001")),
                    It.Is<string>(s => s.Equals("sensors.D0.value"))),
                Times.Once);
        }

        [TestMethod]
        public void SimpleSet()
        {
            string command = "SET Actuator.Hall: True";

            var result = interpreter.Execute(command).Result;

            result.Should().NotBeNull();
            result.ResultCode.Should().Be(Enums.ResultCode.Succsses);

            executor.Verify(exec => exec.Set(
                    It.Is<SourceGroup>(g => g == SourceGroup.Device),
                    It.Is<DeviceDataEntity>(d => d.Id.Equals("SA001") && d.Sensors.D0.Value == true)),
                Times.Once);
        }

        [TestMethod]
        public void SimpleGetAndSet()
        {
            string command = "SET Actuator.Hall: GET: Sensor.Hall;";

            var result = interpreter.Execute(command).Result;

            result.Should().NotBeNull();
            result.ResultCode.Should().Be(Enums.ResultCode.Succsses);

            executor.Verify(exec => exec.Get(
                    It.Is<SourceGroup>(g => g == SourceGroup.Device),
                    It.Is<string>(s => s.Equals("SA001")),
                    It.Is<string>(s => s.Equals("sensors.D0.value"))),
                Times.Once);

            executor.Verify(exec => exec.Set(
                    It.Is<SourceGroup>(g => g == SourceGroup.Device),
                    It.Is<DeviceDataEntity>(d => d.Id.Equals("SA001") && d.Sensors.D0.Value == true)),
                Times.Once);
        }

        [TestMethod]
        public void MultipleStatement()
        {
            string command = @"VAR test = GET: Sensor.Hall;
                               SET Actuator.Hall: test";

            var result = interpreter.Execute(command).Result;

            result.Should().NotBeNull();
            result.ResultCode.Should().Be(Enums.ResultCode.Succsses);

            executor.Verify(exec => exec.Get(
                    It.Is<SourceGroup>(g => g == SourceGroup.Device),
                    It.Is<string>(s => s.Equals("SA001")),
                    It.Is<string>(s => s.Equals("sensors.D0.value"))),
                Times.Once);

            executor.Verify(exec => exec.Set(
                    It.Is<SourceGroup>(g => g == SourceGroup.Device),
                    It.Is<DeviceDataEntity>(d => d.Id.Equals("SA001") && d.Sensors.D0.Value == true)),
                Times.Once);
        }

        [TestMethod]
        public void ScheduleSimpleStatement()
        {
            string command = @"SCHEDULE ""test1"" ON 30 SEC : SET Actuator.Hall: test;";

            var result = interpreter.Execute(command).Result;

            result.Should().NotBeNull();
            result.ResultCode.Should().Be(Enums.ResultCode.Succsses);

            executor.Verify(exec => exec.Schedule(
                    It.Is<string>(s => s.Equals("test1")),
                    It.Is<double>(d => d == 30),
                    It.Is<TimeUnit>(t => t == TimeUnit.SEC),
                    It.Is<string>(s => s.Equals("SET Actuator.Hall: test;")),
                    It.Is<List<Variable>>(v => !v.Any())),
                Times.Once);
        }

        [TestMethod]
        public void ScheduleMultiStatements()
        {
            string command = @"SCHEDULE ""test1"" ON 30 SEC : {
                                VAR test = GET: Sensor.Hall;
                                SET Actuator.Hall: test;
                               }";

            var result = interpreter.Execute(command).Result;

            result.Should().NotBeNull();
            result.ResultCode.Should().Be(Enums.ResultCode.Succsses);

            executor.Verify(exec => exec.Schedule(
                    It.Is<string>(s => s.Equals("test1")),
                    It.Is<double>(d => d == 30),
                    It.Is<TimeUnit>(t => t == TimeUnit.SEC),
                    It.Is<string>(s => s.Equals(@"VAR test = GET: Sensor.Hall;SET Actuator.Hall: test;")),
                    It.Is<List<Variable>>(v => !v.Any())),
                Times.Once);
        }

        [TestMethod]
        public void ScheduleMultiStatementsWithVariable()
        {
            string command = @"VAR test = GET: Sensor.Hall;
                               SCHEDULE ""test1"" ON 2 MIN : SET Actuator.Hall: test;";

            var result = interpreter.Execute(command).Result;

            result.Should().NotBeNull();
            result.ResultCode.Should().Be(Enums.ResultCode.Succsses);

            executor.Verify(exec => exec.Get(
                    It.Is<SourceGroup>(g => g == SourceGroup.Device),
                    It.Is<string>(s => s.Equals("SA001")),
                    It.Is<string>(s => s.Equals("sensors.D0.value"))),
                Times.Once);

            executor.Verify(exec => exec.Schedule(
                    It.Is<string>(s => s.Equals("test1")),
                    It.Is<double>(d => d == 2),
                    It.Is<TimeUnit>(t => t == TimeUnit.MIN),
                    It.Is<string>(s => s.Equals(@"SET Actuator.Hall: test;")),
                    It.Is<List<Variable>>(v =>
                        v.Count == 1 &&
                        v.First().Name.Equals("testVar") &&
                        v.First().Value.Equals("false") &&
                        v.First().Type == VariableType.Boolean)),
                Times.Once);
        }

        private void MockSource()
        {
            devices = new List<DeviceDataEntity>
            {
                new() {
                    Id = "AA001",
                    DeviceName = "AA001",
                    Actuators = new IoGroupEntity
                    {
                        D0 = new() {Description = "Hall", Value = false},
                        D1 = new() {Description = "Kitchen", Value = false},
                        D2 = new() {Description = "Dinner", Value = false},
                        D3 = new() {Description = "Tv", Value = false},
                        D4 = new() {Description = "Door", Value = false},
                        D5 = new() {Description = "Office", Value = false},
                        D6 = new() {Description = "Bedroom", Value = false},
                        D7 = new() {Description = "Bathroom", Value = false},
                        D8 = new() {Description = "Corridor", Value = false}
                    },
                    Sensors = new IoGroupEntity
                    {
                        A0 = new() {Description = "Temperature", Value = 2.0 }
                    },
                    Datetime = System.DateTime.Now
                },
                new() {
                    Id = "SA001",
                    DeviceName = "SA001",
                    Actuators = new IoGroupEntity(),
                    Sensors = new IoGroupEntity
                    {
                        D0 = new() {Description = "Hall", Value = false},
                        D1 = new() {Description = "Kitchen", Value = false},
                        D2 = new() {Description = "Dinner", Value = false},
                        D3 = new() {Description = "Tv", Value = false},
                        D4 = new() {Description = "Door", Value = false},
                        D5 = new() {Description = "Office", Value = false},
                        D6 = new() {Description = "Bedroom", Value = false},
                        D7 = new() {Description = "Bathroom", Value = false},
                        D8 = new() {Description = "Corridor", Value = false},
                        A0 = new() {Description = "Temperature", Value = 2.0 }
                    },
                    Datetime = System.DateTime.Now
                }
            };

            executor.Setup(exec => exec.ListDevices()).ReturnsAsync(devices);

            schedules = new();

            executor.Setup(exec => exec.ListSchedules()).ReturnsAsync(schedules);

            blackboard = new List<BlackboardEntity>
            {
                new(){ Id = "Daylight", Value = "True" },
                new(){ Id = "Rain", Value = "False" },
                new(){ Id = "Temperature", Value = "18" }
            };

            executor
                .Setup(exec => exec.ListBlackboard())
                .ReturnsAsync(blackboard);

            executor
                .Setup(exec => exec.GetBlackboardValue(It.IsAny<string>()))
                .ReturnsAsync((string key) => blackboard.FirstOrDefault(b => b.Id.Equals(key)));

            executor
                .Setup(exec => exec.SetBlackboardValue(It.IsAny<string>(), It.IsAny<string>()))
                .Callback((string key, string value) =>
                {
                    var item = blackboard.FirstOrDefault(b => b.Id.Equals(key));
                    if (item != null)
                        item.Value = value;
                    else
                        blackboard.Add(new BlackboardEntity { Id = key, Value = value });
                });


        }
    }
}