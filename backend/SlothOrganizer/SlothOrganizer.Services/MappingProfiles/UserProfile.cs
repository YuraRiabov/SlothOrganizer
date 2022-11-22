using AutoMapper;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Domain.Entities;

namespace SlothOrganizer.Services.MappingProfiles
{
    public class UserProfile : Profile
    {
        UserProfile()
        {
            CreateMap<NewUserDto, User>();
        }
    }
}
