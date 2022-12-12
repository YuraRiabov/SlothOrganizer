using Dapper;

namespace SlothOrganizer.Persistence
{
    public class DatabaseManager
    {
        private readonly DapperContext _context;
        public DatabaseManager(DapperContext context)
        {
            _context = context;
        }

        public void CreateIfAbsent(string name)
        {
            var query = "SELECT * FROM sys.databases WHERE name = @name";
            var parameters = new DynamicParameters();
            parameters.Add("name", name);
            using (var connection = _context.CreateMasterConnection())
            {
                var records = connection.Query(query, parameters);
                if (!records.Any())
                {
                    connection.Execute($"CREATE DATABASE {name}");
                }
            }
        }
    }
}
