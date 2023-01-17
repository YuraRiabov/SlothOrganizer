using SlothOrganizer.Contracts.DTO.User;

namespace SlothOrganizer.Services.Abstractions.Users
{
    public interface IUserInfoService
    {
        Task DeleteAvatar(long userId);
        Task Update(UpdateUserDto updateUserDto);
    }
}
