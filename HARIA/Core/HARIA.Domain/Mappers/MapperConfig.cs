using AutoMapper;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;

namespace HARIA.Domain.Mappers
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            ConfigureAction();
            ConfigureActionPeriod();
            ConfigureActuator();
            ConfigureAmbient();
            ConfigureDevice();
            ConfigureExternalActuator();
            ConfigureExternalSensor();
            ConfigureLog();
            ConfigureScenario();
            ConfigureScenarioTrigger();
            ConfigureSensor();
            ConfigureUser();
        }

        private void ConfigureUser()
        {
            CreateMap<User, UserEntity>();
            CreateMap<UserEntity, User>()
                .ForMember(
                dest => dest.PasswordHash,
                opt => opt.Ignore());
        }

        private void ConfigureSensor()
        {
            CreateMap<Sensor, SensorEntity>()
                .ReverseMap();
        }

        private void ConfigureScenarioTrigger()
        {
            CreateMap<ScenarioTrigger, ScenarioTriggerEntity>()
                .ReverseMap();
        }

        private void ConfigureScenario()
        {
            CreateMap<Scenario, ScenarioEntity>()
                .ReverseMap();
        }

        private void ConfigureLog()
        {
            CreateMap<Log, LogEntity>()
                .ReverseMap();
        }

        private void ConfigureExternalSensor()
        {
            CreateMap<ExternalSensor, ExternalSensorEntity>()
                .ReverseMap();
        }

        private void ConfigureExternalActuator()
        {
            CreateMap<ExternalActuator, ExternalActuatorEntity>()
                .ReverseMap();
        }

        private void ConfigureDevice()
        {
            CreateMap<Device, DeviceEntity>()
                .ReverseMap();
        }

        private void ConfigureAmbient()
        {
            CreateMap<Ambient, AmbientEntity>()
                .ReverseMap();
        }

        private void ConfigureActuator()
        {
            CreateMap<Actuator, ActuatorEntity>()
                .ReverseMap();
        }

        private void ConfigureActionPeriod()
        {
            CreateMap<ActionEventPeriod, ActionEventPeriodEntity>()
                .ReverseMap();
        }

        private void ConfigureAction()
        {
            CreateMap<ActionEvent, ActionEventEntity>()
                .ReverseMap();
        }
    }
}