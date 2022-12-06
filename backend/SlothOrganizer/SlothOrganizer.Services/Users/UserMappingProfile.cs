using AutoMapper;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Domain.Entities;

namespace SlothOrganizer.Services.Users
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<NewUserDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
