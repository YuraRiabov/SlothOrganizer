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
            var id = GetCurrentUserId();
            if (id is null)
            {
                return Unauthorized();
            }
            return Ok(await _dashboardService.Create(newDashboard, (long)id));
        }

        [HttpGet]
        public async Task<ActionResult<List<DashboardDto>>> Get()
        {
            var id = GetCurrentUserId();
            if (id is null)
            {
                return Unauthorized();
            }
            return Ok(await _dashboardService.Get((long)id));
        }

        private long? GetCurrentUserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (!long.TryParse(identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id))
            {
                return null;
            }
            return id;
        }
    }
}
