using Microsoft.AspNetCore.Mvc;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Services.Abstractions;

namespace SlothOrganizer.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] NewUserDto newUserDto)
        {
            await _userService.CreateUser(newUserDto);
            return Ok();
        }
    }
}
