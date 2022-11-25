using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Services.Abstractions;

namespace SlothOrganizer.Services
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

        public async Task<UserDto> SignUp(NewUserDto newUser)
        {
            var user = await _userService.CreateUser(newUser);
            var code = await _verificationCodeService.GenerateCode(user.Id);
            try
            {
                await _emailService.SendEmail(user.Email, "Verify your email", $"Your verification code is {code}");
            }
            catch (Exception) { }
            return user;
        }
    }
}
