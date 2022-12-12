using SlothOrganizer.Contracts.DTO.User;

namespace SlothOrganizer.Services.Abstractions.Users
{
    public interface IUserService
    {
        Task<UserDto> Create(NewUserDto newUser);

        Task<UserDto> Get(long id);
        Task<string?> VerifyEmail(long userId, int code);
    }
}
