﻿using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<UserDto>> SignUp([FromBody] NewUserDto newUserDto)
        {
            var user = await _authService.SignUp(newUserDto);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPut("verifyEmail")]
        public async Task<ActionResult<TokenDto>> VerifyEmail([FromBody] VerificationCodeDto verificationCode)
        {
            return Ok(await _authService.VerifyEmail(verificationCode));
        }

        [AllowAnonymous]
        [HttpPost("resendCode/{userId}")]
        public async Task<IActionResult> ResendVerificationCode(long userId)
        {
            await _authService.ResendVerificationCode(userId);
            return Ok();
        }
    }
}
