using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Services.Abstractions.Auth;

namespace SlothOrganizer.Presentation.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("sign-up")]
        public async Task<UserDto> SignUp([FromBody] NewUserDto newUserDto)
        {
            var user = await _authService.SignUp(newUserDto);
            return user;
        }

        [HttpPut("verify-email")]
        public async Task<UserAuthDto> VerifyEmail([FromBody] VerificationCodeDto verificationCode)
        {
            return await _authService.VerifyEmail(verificationCode);
        }

        [HttpPost("sign-in")]
        public async Task<UserAuthDto> SignIn([FromBody] LoginDto loginDto)
        {
            return await _authService.SignIn(loginDto);
        }

        [HttpPost("resend-code/{email}")]
        public async Task ResendVerificationCode(string email)
        {
            await _authService.ResendVerificationCode(email);
        }

        [HttpPut("refresh-token")]
        public async Task<TokenDto> RefreshToken([FromBody] TokenDto token)
        {
            return await _authService.RefreshToken(token);
        }

        [HttpPut("resetPassword")]
        public async Task<UserAuthDto> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            return await _authService.ResetPassword(resetPasswordDto);
        }

        [HttpPost("sendPasswordReset/{email}")]
        public async Task SendPasswordReset(string email)
        {
            await _authService.SendResetPassword(email);
        }
    }
}
