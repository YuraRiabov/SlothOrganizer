using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Services.Abstractions.Users;

namespace SlothOrganizer.Presentation.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("user-credentials")]
    public class UserCredentialsController
    {
        private readonly IUserCredentialsService _userCredentialsService;

        public UserCredentialsController(IUserCredentialsService userCredentialsService)
        {
            _userCredentialsService = userCredentialsService;
        }

        [HttpPost("sign-up")]
        public async Task<UserDto> SignUp([FromBody] NewUserDto newUserDto)
        {
            var user = await _userCredentialsService.Create(newUserDto);
            return user;
        }

        [HttpPut("verify-email")]
        public async Task<UserAuthDto> VerifyEmail([FromBody] VerificationCodeDto verificationCode)
        {
            return await _userCredentialsService.VerifyEmail(verificationCode);
        }

        [HttpPost("sign-in")]
        public async Task<UserAuthDto> SignIn([FromBody] LoginDto loginDto)
        {
            return await _userCredentialsService.SignIn(loginDto);
        }

        [HttpPut("reset-password")]
        public async Task<UserAuthDto> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            return await _userCredentialsService.ResetPassword(resetPasswordDto);
        }
    }
}
