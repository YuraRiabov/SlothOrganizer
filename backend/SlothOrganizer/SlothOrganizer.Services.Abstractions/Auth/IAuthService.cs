using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;

namespace SlothOrganizer.Services.Abstractions.Auth
{
    public interface IAuthService
    {
        Task<UserDto> SignUp(NewUserDto newUser);
        Task<UserAuthDto> SignIn(AuthorizationDto authorizationDto);

        Task<TokenDto> VerifyEmail(VerificationCodeDto verificationCode);

        Task ResendVerificationCode(long userId);

        Task<TokenDto> RefreshToken(TokenDto expiredToken);
    }
}
