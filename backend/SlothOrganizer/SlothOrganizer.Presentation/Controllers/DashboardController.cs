using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlothOrganizer.Contracts.DTO.Tasks.Dashboard;
using SlothOrganizer.Services.Abstractions.Tasks;

namespace SlothOrganizer.Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("dashboards")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpPost]
        public async Task<ActionResult<DashboardDto>> Create([FromBody] NewDashboardDto newDashboard)
        {
            var email = GetCurrentUserEmail();
            if (email is null)
            {
                return Unauthorized();
            }
            return Ok(await _dashboardService.Create(newDashboard, email));
        }

        [HttpGet]
        public async Task<ActionResult<List<DashboardDto>>> Get()
        {
            var email = GetCurrentUserEmail();
            if (email is null)
            {
                return Unauthorized();
            }
            return Ok(await _dashboardService.Get(email));
        }

        private string? GetCurrentUserEmail()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return identity?.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}
