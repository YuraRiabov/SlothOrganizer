using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlothOrganizer.Contracts.DTO.User;
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

        [HttpDelete("{userId}/avatar")]
        public async Task DeleteAvatar(long userId)
        {
            await _userInfoService.DeleteAvatar(userId);
        }
    }
}
