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
        public async Task<UserDto> SignUp([FromBody] NewUserDto newUserDto)
        {
            var user = await _authService.SignUp(newUserDto);
            return user;
        }

        [AllowAnonymous]
        [HttpPut("verifyEmail")]
        public async Task<TokenDto> VerifyEmail([FromBody] VerificationCodeDto verificationCode)
        {
            return await _authService.VerifyEmail(verificationCode);
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<ActionResult<UserAuthDto>> SignIn([FromBody] AuthorizationDto authorizationDto)
        {
            return Ok(await _authService.SignIn(authorizationDto));
        }

        [AllowAnonymous]
        [HttpPost("resendCode/{userId}")]
        public async Task ResendVerificationCode(long userId)
        {
            await _authService.ResendVerificationCode(userId);
        }

        [AllowAnonymous]
        [HttpPut("refreshToken")]
        public async Task<ActionResult<TokenDto>> RefreshToken([FromBody] TokenDto token)
        {
            return Ok(await _authService.RefreshToken(token));
        }
    }
}
