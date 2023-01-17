using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;

namespace SlothOrganizer.Services.Abstractions.Users
{
    public interface IUserCredentialsService
    {
        Task<UserDto> Create(NewUserDto newUser);
        Task<UserAuthDto> SignIn(LoginDto login);
        Task<UserAuthDto> VerifyEmail(VerificationCodeDto verificationCode);
        Task<UserAuthDto> ResetPassword(ResetPasswordDto resetPasswordDto);
    }
}
