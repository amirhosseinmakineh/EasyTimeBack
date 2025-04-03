using AutoMapper;
using EasyTime.Application.Contract.Dtos;
using EasyTime.Model.Models;

namespace EasyTime.Application.Contract.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();   
        }
    }
}
