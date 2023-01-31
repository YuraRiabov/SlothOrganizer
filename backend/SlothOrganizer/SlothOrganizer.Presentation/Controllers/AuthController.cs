using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlothOrganizer.Contracts.DTO.Auth;
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

        [HttpPost("send-code/{email}")]
        public async Task ResendVerificationCode(string email)
        {
            await _authService.SendVerificationCode(email);
        }

        [HttpPut("refresh-token")]
        public async Task<TokenDto> RefreshToken([FromBody] TokenDto token)
        {
            return await _authService.RefreshToken(token);
        }

        [HttpPost("send-password-reset/{email}")]
        public async Task SendPasswordReset(string email)
        {
            await _authService.SendResetPassword(email);
        }
    }
}
