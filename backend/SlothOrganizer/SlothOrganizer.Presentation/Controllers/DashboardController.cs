using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlothOrganizer.Contracts.DTO.Tasks.Dashboard;
using SlothOrganizer.Presentation.Extensions;
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
        public async Task<DashboardDto> Create([FromBody] NewDashboardDto newDashboard)
        {

            var id = HttpContext.User.GetId();
            return await _dashboardService.Create(newDashboard, id);
        }

        [HttpGet]
        public async Task<List<DashboardDto>> Get()
        {
            var id = HttpContext.User.GetId();
            return await _dashboardService.Get(id);
        }
    }
}
