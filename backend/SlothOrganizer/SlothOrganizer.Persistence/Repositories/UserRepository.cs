using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Repositories;

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
            var command = "DELETE FROM Users WHERE Id=@Id";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(command, new { id });
        }

        public Task<IEnumerable<User>> GetAll()
        {
            var query = "SELECT * FROM Users";

            using var connection = _dapperContext.CreateConnection();
            return connection.QueryAsync<User>(query);
        }

        public async Task<User?> GetByEmail(string email)
        {
            var query = "SELECT TOP(1) * FROM Users WHERE Email=@Email";

            using var connection = _dapperContext.CreateConnection();
            var result = await connection.QuerySingleOrDefaultAsync<User?>(query, new { email });
            return result;
        }

        public async Task<User?> GetById(long id)
        {
            var query = "SELECT TOP(1) * FROM Users WHERE Id=@Id";

            using var connection = _dapperContext.CreateConnection();
            var result = await connection.QuerySingleOrDefaultAsync<User?>(query, new { id });
            return result;
        }

        public async Task<User> Insert(User user)
        {
            var query = "INSERT INTO Users (FirstName, LastName, Email, Password, Salt, EmailVerified)" +
                " VALUES (@FirstName, @LastName, @Email, @Password, @Salt, @EmailVerified)" +
                " SELECT CAST(SCOPE_IDENTITY() as bigint)";

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
            var command = "UPDATE Users SET FirstName=@FirstName, LastName=@LastName, Email=@Email," +
                " Password=@Password, Salt=@Salt, EmailVerified=@EmailVerified WHERE Id=@Id";

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
    }
}
