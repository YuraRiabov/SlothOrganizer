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

        public async Task<Dashboard> Insert(string title, string userEmail)
        {
            var query = Resources.InsertDashboard;

            var parameters = new
            {
                Email = userEmail,
                Title = title,
            };

            using var connection = _context.CreateConnection();
            return await connection.QuerySingleAsync<Dashboard>(query, parameters);
        }

        public async Task<List<Dashboard>> Get(string userEmail)
        {
            var query = Resources.GetAllDashboards;

            using var connection = _context.CreateConnection();
            var dashboards = await connection.QueryAsync<Dashboard>(query, new { Email = userEmail });
            return dashboards.ToList();
        }
    }
}
