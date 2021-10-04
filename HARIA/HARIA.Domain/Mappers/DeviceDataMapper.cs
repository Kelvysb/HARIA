using AutoMapper;
using HARIA.Domain.Entities;
using HARIA.Domain.Models;

namespace HARIA.Domain.Mappers
{
    internal class DeviceDataMapper : Profile
    {
        public DeviceDataMapper()
        {
            MapAnalogElement();
            MapDigitalElement();
            MapIoGroup();
            MapDeviceData();
        }

        private void MapDeviceData()
        {
            CreateMap<DeviceDataEntity, DeviceData>()
                .ForMember(dest => dest.DeviceId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
        }

        private void MapIoGroup()
        {
            CreateMap<IoGroupEntity, IoGroup>()
               .ReverseMap();
        }

        private void MapDigitalElement()
        {
            CreateMap<DigitalElementEntity, DigitalElement>()
               .ReverseMap();
        }

        private void MapAnalogElement()
        {
            CreateMap<AnalogElementEntity, AnalogElement>()
               .ReverseMap();
        }
    }
}