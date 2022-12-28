using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Services.Abstractions.Auth;

namespace SlothOrganizer.Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("sign-up")]
        public async Task<UserDto> SignUp([FromBody] NewUserDto newUserDto)
        {
            var user = await _authService.SignUp(newUserDto);
            return user;
        }

        [AllowAnonymous]
        [HttpPut("verify-email")]
        public async Task<TokenDto> VerifyEmail([FromBody] VerificationCodeDto verificationCode)
        {
            return await _authService.VerifyEmail(verificationCode);
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<UserAuthDto> SignIn([FromBody] LoginDto loginDto)
        {
            return await _authService.SignIn(loginDto);
        }

        [AllowAnonymous]
        [HttpPost("resend-code/{userId}")]
        public async Task ResendVerificationCode(long userId)
        {
            await _authService.ResendVerificationCode(userId);
        }

        [AllowAnonymous]
        [HttpPut("refresh-token")]
        public async Task<TokenDto> RefreshToken([FromBody] TokenDto token)
        {
            return await _authService.RefreshToken(token);
        }
    }
}
