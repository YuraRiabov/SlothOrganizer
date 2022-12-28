using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlothOrganizer.Contracts.DTO.Tasks.Dashboard;
using SlothOrganizer.Services.Abstractions.Tasks;

namespace SlothOrganizer.Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpPost]
        public async Task<DashboardDto> Create([FromBody] NewDashboardDto newDashboard)
        {
            return await _dashboardService.Create(newDashboard);
        }

        [HttpGet("{userId}")]
        public async Task<List<DashboardDto>> Get(long userId)
        {
            return await _dashboardService.Get(userId);
        }
    }
}
