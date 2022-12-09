using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;

namespace SlothOrganizer.Services.Abstractions.Users
{
    public interface IUserService
    {
        Task<UserDto> Create(NewUserDto newUser);
        Task<UserDto> Authorize(LoginDto login);

        Task<UserDto> Get(long id);
        Task<UserDto?> VerifyEmail(string email, int code);
        Task<UserDto> Get(string email);
    }
}
