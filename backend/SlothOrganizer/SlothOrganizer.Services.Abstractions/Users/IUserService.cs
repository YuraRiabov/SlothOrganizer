using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlothOrganizer.Contracts.DTO.User;

namespace SlothOrganizer.Services.Abstractions.Users
{
    public interface IUserService
    {
        Task<UserDto> CreateUser(NewUserDto newUser);

        Task<UserDto> GetUser(long id);

        Task VerifyEmail(long userId);
    }
}
