using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlothOrganizer.Contracts.DTO.User;

namespace SlothOrganizer.Services.Abstractions
{
    public interface IAuthService
    {
        Task<UserDto> SignUp(NewUserDto newUser);
    }
}
