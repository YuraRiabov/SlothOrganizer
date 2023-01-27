using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Presentation.Extensions;
using SlothOrganizer.Services.Abstractions.Users;

namespace SlothOrganizer.Presentation.Controllers
{
    [Authorize]
    [Route("users-info")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserInfoService _userInfoService;

        public UserInfoController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        [HttpPut]
        public async Task Update([FromBody] UpdateUserDto updateUserDto)
        {
            await _userInfoService.Update(updateUserDto);
        }

        [HttpPut("avatar")]
        public async Task<UserDto> Update([FromForm] IFormFile avatar)
        {
            var avatarBytes = avatar.GetBytes();
            var userId = HttpContext.User.GetId();
            return await _userInfoService.UpdateAvatar(userId, avatarBytes, avatar.FileName);
        }

        [HttpDelete("avatar")]
        public async Task DeleteAvatar()
        {
            var userId = HttpContext.User.GetId();
            await _userInfoService.DeleteAvatar(userId);
        }
    }
}
