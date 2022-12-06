using Dapper;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Persistence.Properties;

namespace SlothOrganizer.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _dapperContext;

        public UserRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task Delete(long id)
        {
            var command = Resources.DeleteUser;

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(command, new { id });
        }

        public Task<IEnumerable<User>> GetAll()
        {
            var query = Resources.SelectAllUsers;

            using var connection = _dapperContext.CreateConnection();
            return connection.QueryAsync<User>(query);
        }

        public async Task<User?> Get(string email)
        {
            var query = Resources.GetUserByEmail;

            using var connection = _dapperContext.CreateConnection();
            var result = await connection.QuerySingleOrDefaultAsync<User?>(query, new { email });
            return result;
        }

        public async Task<User?> Get(long id)
        {
            var query = Resources.GetByUserId;

            using var connection = _dapperContext.CreateConnection();
            var result = await connection.QuerySingleOrDefaultAsync<User?>(query, new { id });
            return result;
        }

        public async Task<User> Insert(User user)
        {
            var query = Resources.InsertUser;

            var parameters = new
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Salt = user.Salt,
                EmailVerified = user.EmailVerified
            };

            using var connection = _dapperContext.CreateConnection();
            var id = await connection.QuerySingleAsync<long>(query, parameters);
            user.Id = id;
            return user;
        }

        public async Task Update(User user)
        {
            var command = Resources.UpdateUser;

            var parameters = new
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Salt = user.Salt,
                EmailVerified = user.EmailVerified,
                Id = user.Id,
            };

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(command, parameters);
        }

        public async Task<string?> VerifyEmail(long id, int code)
        {
            var command = Resources.VerifyEmailByCode;

            var parameters = new
            {
                Id = id,
                Code = code
            };

            using var connection = _dapperContext.CreateConnection();
            return await connection.QuerySingleAsync<string?>(command, parameters);
        }
    }
}
