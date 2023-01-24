using AutoMapper;
using SlothOrganizer.Contracts.DTO.Tasks.Dashboard;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Tasks;

namespace SlothOrganizer.Services.Tasks
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IMapper _mapper;

        public DashboardService(IDashboardRepository dashboardRepository, IMapper mapper)
        {
            _dashboardRepository = dashboardRepository;
            _mapper = mapper;
        }

        public async Task<DashboardDto> Create(NewDashboardDto dashboardDto)
        {
            var newDashboard = _mapper.Map<Dashboard>(dashboardDto);
            var dashboard = await _dashboardRepository.Insert(newDashboard);
            return _mapper.Map<DashboardDto>(dashboard);
        }

        public async Task<List<DashboardDto>> Get(long userId)
        {
            var dashboards = await _dashboardRepository.Get(userId);
            if (dashboards.Count == 0)
            {
                foreach (var dashboard in GetDefault(userId))
                { 
                    dashboards.Add(await _dashboardRepository.Insert(dashboard));
                }
            }
            return _mapper.Map<List<DashboardDto>>(dashboards);
        }

        private List<Dashboard> GetDefault(long userId)
        {
            return new List<Dashboard>
            {
                new Dashboard
                {
                    UserId = userId,
                    Title = "Routine"
                },
                new Dashboard
                {
                    UserId = userId,
                    Title = "Work"
                }
            };
        }
    }
}
