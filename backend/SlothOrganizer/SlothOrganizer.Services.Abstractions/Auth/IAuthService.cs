using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;

namespace SlothOrganizer.Services.Abstractions.Auth
{
    public interface IAuthService
    {
        Task SendVerificationCode(string email);

        Task<TokenDto> RefreshToken(TokenDto expiredToken);
        Task SendResetPassword(string email);
    }
}
