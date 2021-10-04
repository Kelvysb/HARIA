using AutoMapper;
using HARIA.Domain.Entities;
using HARIA.Domain.Models;

namespace HARIA.Domain.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            MapUser();
        }

        private void MapUser()
        {
            CreateMap<UserEntity, User>()
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.NewPassword, opt => opt.Ignore())
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}