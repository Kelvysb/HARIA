using System.Collections.Generic;
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
            ConfigureNode();
            ConfigureExternalActuator();
            ConfigureExternalSensor();
            ConfigureScenario();
            ConfigureScenarioTrigger();
            ConfigureSensor();
            ConfigureUser();
            ConfigureRole();
            ConfigurePermission();
            ConfigureState();
        }

        private void ConfigureState()
        {
            CreateMap<State, StateEntity>()
                .ReverseMap();

            CreateMap<State, KeyValuePair<string, string>>()
                .ConvertUsing(c => new KeyValuePair<string, string>(c.Key, c.Value));

            CreateMap<KeyValuePair<string, string>, State>()
                .ConvertUsing(c => new State()
                {
                    Id = 0,
                    Key = c.Key,
                    Value = c.Value,
                    DefaultValue = string.Empty,
                    IsSystemDefault = false
                });

            CreateMap<StateEntity, KeyValuePair<string, string>>()
                .ConvertUsing(c => new KeyValuePair<string, string>(c.Key, c.Value));

            CreateMap<KeyValuePair<string, string>, StateEntity>()
                .ConvertUsing(c => new StateEntity()
                {
                    Id = 0,
                    Key = c.Key,
                    Value = c.Value,
                    DefaultValue = string.Empty,
                    IsSystemDefault = false
                });
        }

        private void ConfigurePermission()
        {
            CreateMap<Permission, PermissionEntity>()
                .ReverseMap();
        }

        private void ConfigureRole()
        {
            CreateMap<Role, RoleEntity>()
                .ReverseMap();
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

        private void ConfigureNode()
        {
            CreateMap<Node, NodeEntity>()
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