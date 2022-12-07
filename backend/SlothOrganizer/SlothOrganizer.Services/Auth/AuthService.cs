using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Services.Abstractions.Auth;
using SlothOrganizer.Services.Abstractions.Auth.Tokens;
using SlothOrganizer.Services.Abstractions.Email;
using SlothOrganizer.Services.Abstractions.Users;

namespace SlothOrganizer.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IAccessTokenService _accessTokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IVerificationCodeService _verificationCodeService;
        private readonly IEmailService _emailService;

        public AuthService(IAccessTokenService tokenService,
            IUserService userService,
            IVerificationCodeService verificationCodeService,
            IEmailService emailService,
            IRefreshTokenService refreshTokenService)
        {
            _accessTokenService = tokenService;
            _userService = userService;
            _verificationCodeService = verificationCodeService;
            _emailService = emailService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task ResendVerificationCode(long userId)
        {
            var user = await _userService.Get(userId);
            await SendVerificationCode(user);
        }

        public async Task<TokenDto> RefreshToken(TokenDto expiredToken)
        {
            var email = _accessTokenService.GetEmailFromToken(expiredToken.AccessToken);
            if (await _refreshTokenService.Validate(email, expiredToken.RefreshToken))
            {
                return await GenerateToken(email);
            }
            throw new InvalidCredentialsException("Invalid refresh token");
        }

        public async Task<UserAuthDto> SignIn(LoginDto login)
        {
            var user = await _userService.Authorize(login);
            if (!user.EmailVerified)
            {
                return new UserAuthDto
                {
                    User = user
                };
            }
            var token = await GenerateToken(user.Email);
            return new UserAuthDto
            {
                User = user,
                Token = token
            };
        }

        public async Task<UserDto> SignUp(NewUserDto newUser)
        {
            var user = await _userService.Create(newUser);
            await SendVerificationCode(user);
            return user;
        }

        public async Task<TokenDto> VerifyEmail(VerificationCodeDto verificationCode)
        {
            var email = await _userService.VerifyEmail(verificationCode.UserId, verificationCode.VerificationCode);
            if (email is not null)
            {
                return await GenerateToken(email);
            }
            throw new InvalidCredentialsException("Invalid verification code");
        }

        private async Task SendVerificationCode(UserDto user)
        {
            var code = await _verificationCodeService.Generate(user.Id);
            await _emailService.SendEmail(user.Email, "Verify your email", $"Your verification code is {code}");
        }

        private async Task<TokenDto> GenerateToken(string email)
        {
            return new TokenDto
            {
                AccessToken = _accessTokenService.Generate(email),
                RefreshToken = await _refreshTokenService.Generate(email)
            };
        }
    }
}
