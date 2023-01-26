using SlothOrganizer.Contracts.DTO.Tasks.Dashboard;

namespace SlothOrganizer.Services.Abstractions.Tasks
{
    public interface IDashboardService
    {
        Task<DashboardDto> Create(NewDashboardDto dashboardDto, long userId);
        Task<List<DashboardDto>> Get(long userId);
    }
}
