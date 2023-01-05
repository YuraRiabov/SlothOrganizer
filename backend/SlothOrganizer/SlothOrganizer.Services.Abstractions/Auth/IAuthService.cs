using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;

namespace SlothOrganizer.Services.Abstractions.Auth
{
    public interface IAuthService
    {
        Task<UserDto> SignUp(NewUserDto newUser);
        Task<UserAuthDto> SignIn(LoginDto login);

        Task<UserAuthDto> VerifyEmail(VerificationCodeDto verificationCode);

        Task ResendVerificationCode(string email);

        Task<TokenDto> RefreshToken(TokenDto expiredToken);
        Task<UserAuthDto> ResetPassword(ResetPasswordDto resetPasswordDto);
        Task SendResetPassword(string email);
    }
}
