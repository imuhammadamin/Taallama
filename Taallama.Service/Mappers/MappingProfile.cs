using AutoMapper;
using Taallama.Domain.Entities;
using Taallama.Service.DTOs;

namespace Taallama.Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<VideoDTO, Video>().ReverseMap();
            CreateMap<CourseDTO, Course>().ReverseMap();
        }
    }
}