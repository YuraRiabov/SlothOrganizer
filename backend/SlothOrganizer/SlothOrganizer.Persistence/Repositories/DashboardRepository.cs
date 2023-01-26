using Dapper;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Persistence.Properties;

namespace SlothOrganizer.Persistence.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly DapperContext _context;

        public DashboardRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Dashboard> Insert(Dashboard dashboard)
        {
            var query = Resources.InsertDashboard;

            var parameters = new
            {
                UserId = dashboard.UserId,
                Title = dashboard.Title,
            };

            using var connection = _context.CreateConnection();
            dashboard.Id = await connection.QuerySingleAsync<long>(query, parameters);
            return dashboard;
        }

        public async Task<List<Dashboard>> Get(long userId)
        {
            var query = Resources.GetAllDashboards;

            using var connection = _context.CreateConnection();
            var dashboards = await connection.QueryAsync<Dashboard>(query, new { userId });
            return dashboards.ToList();
        }
    }
}
