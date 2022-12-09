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
        [HttpPost("signup")]
        public async Task<UserDto> SignUp([FromBody] NewUserDto newUserDto)
        {
            var user = await _authService.SignUp(newUserDto);
            return user;
        }

        [AllowAnonymous]
        [HttpPut("verifyEmail")]
        public async Task<UserAuthDto> VerifyEmail([FromBody] VerificationCodeDto verificationCode)
        {
            return await _authService.VerifyEmail(verificationCode);
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<UserAuthDto> SignIn([FromBody] LoginDto loginDto)
        {
            return await _authService.SignIn(loginDto);
        }

        [AllowAnonymous]
        [HttpPost("resendCode/{email}")]
        public async Task ResendVerificationCode(string email)
        {
            await _authService.ResendVerificationCode(email);
        }

        [AllowAnonymous]
        [HttpPut("refreshToken")]
        public async Task<TokenDto> RefreshToken([FromBody] TokenDto token)
        {
            return await _authService.RefreshToken(token);
        }
    }
}
