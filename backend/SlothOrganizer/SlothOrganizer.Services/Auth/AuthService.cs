using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Services.Abstractions.Auth;
using SlothOrganizer.Services.Abstractions.Auth.Tokens;
using SlothOrganizer.Services.Abstractions.Auth.UserVerification;
using SlothOrganizer.Services.Abstractions.Users;

namespace SlothOrganizer.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IAccessTokenService _accessTokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IUserVerificationService _userVerificationService;

        public AuthService(IAccessTokenService tokenService,
            IUserService userService,
            IUserVerificationService userVerificationService,
            IRefreshTokenService refreshTokenService)
        {
            _accessTokenService = tokenService;
            _userService = userService;
            _userVerificationService = userVerificationService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task ResendVerificationCode(string email)
        {
            var user = await _userService.Get(email);
            await _userVerificationService.SendVerificationCode(user.Email);
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
            var user = await _userService.Get(login);
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
            await _userVerificationService.SendVerificationCode(user.Email);
            return user;
        }

        public async Task<UserAuthDto> VerifyEmail(VerificationCodeDto verificationCode)
        {
            var user = await _userService.VerifyEmail(verificationCode.Email, verificationCode.VerificationCode);
            if (user is not null)
            {
                return new UserAuthDto
                {
                    User = user,
                    Token = await GenerateToken(user.Email)
                };
            }
            throw new InvalidCredentialsException("Invalid verification code");
        }

        public async Task<UserAuthDto> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userService.ResetPassword(resetPasswordDto);
            return new UserAuthDto
            {
                User = user,
                Token = await GenerateToken(user.Email)
            };
        }

        public async Task SendResetPassword(string email)
        {
            var user = await _userService.Get(email);
            await _userVerificationService.SendPasswordReset(user.Email);
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
