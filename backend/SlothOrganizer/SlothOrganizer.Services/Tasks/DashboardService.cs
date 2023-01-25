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

        public async Task<DashboardDto> Create(NewDashboardDto dashboardDto, string userEmail)
        {
            var dashboard = await _dashboardRepository.Insert(dashboardDto.Title, userEmail);
            return _mapper.Map<DashboardDto>(dashboard);
        }

        public async Task<List<DashboardDto>> Get(string userEmail)
        {
            var dashboards = await _dashboardRepository.Get(userEmail);
            if (dashboards.Count == 0)
            {
                foreach (var title in GetDefaultTitles())
                { 
                    dashboards.Add(await _dashboardRepository.Insert(title, userEmail));
                }
            }
            return _mapper.Map<List<DashboardDto>>(dashboards);
        }

        private List<string> GetDefaultTitles()
        {
            return new List<string> { "Routine", "Work" };
        }
    }
}
