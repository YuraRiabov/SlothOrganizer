using AutoMapper;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Domain.Entities;

namespace SlothOrganizer.Services.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<NewUserDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
