using AutoMapper;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Domain.Entities;

namespace SlothOrganizer.Services.Users
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
