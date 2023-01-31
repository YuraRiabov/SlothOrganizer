using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Services.Abstractions.Auth;
using SlothOrganizer.Services.Abstractions.Auth.Tokens;
using SlothOrganizer.Services.Abstractions.Auth.UserVerification;

namespace SlothOrganizer.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly INotificationService _notificationService;

        public AuthService(ITokenService tokenService, INotificationService notificationService)
        {
            _notificationService = notificationService;
            _tokenService = tokenService;
        }

        public async Task SendVerificationCode(string email)
        {
            await _notificationService.SendVerificationCode(email);
        }

        public async Task<TokenDto> RefreshToken(TokenDto expiredToken)
        {
            var email = _tokenService.GetEmail(expiredToken);
            var id = _tokenService.GetId(expiredToken);
            if (await _tokenService.Validate(email, expiredToken))
            {
                return await _tokenService.Generate(email, id);
            }
            throw new InvalidCredentialsException("Invalid refresh token");
        }

        public async Task SendResetPassword(string email)
        {
            await _notificationService.SendPasswordResetLink(email);
        }
    }
}
