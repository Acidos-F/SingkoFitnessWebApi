using AutoMapper;
using SingkoFItnessWebApi.Dtos;
using SingkoFItnessWebApi.Models;

namespace SingkoFitnessAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //CreateMap<User, UserReadDto>();
            CreateMap<User, UsersReadDto>()
    .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName));


            // DTO → Entity
            CreateMap<UsersCreateDto, User>();


            CreateMap<UsersUpdateDto, User>();
        }
    }
}