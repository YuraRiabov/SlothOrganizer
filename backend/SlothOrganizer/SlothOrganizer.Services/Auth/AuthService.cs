using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Services.Abstractions.Auth;
using SlothOrganizer.Services.Abstractions.Email;
using SlothOrganizer.Services.Abstractions.Users;

namespace SlothOrganizer.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IVerificationCodeService _verificationCodeService;
        private readonly IEmailService _emailService;

        public AuthService(ITokenService tokenService, IUserService userService, IVerificationCodeService verificationCodeService, IEmailService emailService)
        {
            _tokenService = tokenService;
            _userService = userService;
            _verificationCodeService = verificationCodeService;
            _emailService = emailService;
        }

        public async Task ResendVerificationCode(long userId)
        {
            var user = await _userService.GetUser(userId);
            await SendVerificationCode(user);
        }

        public async Task<UserDto> SignUp(NewUserDto newUser)
        {
            var user = await _userService.CreateUser(newUser);
            await SendVerificationCode(user);
            return user;
        }

        public async Task<TokenDto> VerifyEmail(VerificationCodeDto verificationCode)
        {
            var user = await _userService.GetUser(verificationCode.UserId);
            if (await _verificationCodeService.VerifyCode(verificationCode.UserId, verificationCode.VerificationCode))
            {
                await _userService.VerifyEmail(user.Id);
                return _tokenService.GenerateToken(user.Email);
            }
            throw new InvalidCredentialsException("Invalid verification code");
        }

        private async Task SendVerificationCode(UserDto user)
        {
            var code = await _verificationCodeService.GenerateCode(user.Id);
            await _emailService.SendEmail(user.Email, "Verify your email", $"Your verification code is {code}");
        }
    }
}
