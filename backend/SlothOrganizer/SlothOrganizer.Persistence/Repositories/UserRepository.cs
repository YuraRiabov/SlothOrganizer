﻿using System;
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

        public Task<User?> GetByEmail(string email)
        {
            var query = "SELECT TOP(1) * FROM Users WHERE Email=@Email";

            using var connection = _dapperContext.CreateConnection();
            return connection.QuerySingleOrDefaultAsync<User?>(query, new { email });
        }

        public Task<User?> GetById(long id)
        {
            var query = "SELECT TOP(1) * FROM Users WHERE Id=@Id";

            using var connection = _dapperContext.CreateConnection();
            return connection.QuerySingleOrDefaultAsync<User?>(query, new { id });
        }

        public async Task<User> Insert(User user)
        {
            var query = "INSERT INTO Users (FirstName, LastName, Email, Password, Salt, EmailVerified, RefreshToken)" +
                " VALUES (@FirstName, @LastName, @Email, @Password, @Salt, @EmailVerified, @RefreshToken)" +
                " SELECT CAST(SCOPE_IDENTITY() as bigint)";

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", user.FirstName);
            parameters.Add("LastName", user.LastName);
            parameters.Add("Email", user.Email);
            parameters.Add("Password", user.Password);
            parameters.Add("Salt", user.Salt);
            parameters.Add("EmailVerified", user.EmailVerified);
            parameters.Add("RefreshToken", user.RefreshToken);

            using var connection = _dapperContext.CreateConnection();
            var id = await connection.QuerySingleAsync<long>(query, parameters);
            user.Id = id;
            return user;
        }

        public async Task Update(User user)
        {
            var command = "UPDATE Users SET FirstName=@FirstName, LastName=@LastName, Email=@Email," +
                " Password=@Password, Salt=@Salt, EmailVerified=@EmailVerified, RefreshToken=@RefreshToken)";

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", user.FirstName);
            parameters.Add("LastName", user.LastName);
            parameters.Add("Email", user.Email);
            parameters.Add("Password", user.Password);
            parameters.Add("Salt", user.Salt);
            parameters.Add("EmailVerified", user.EmailVerified);
            parameters.Add("RefreshToken", user.RefreshToken);

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(command, parameters);
        }
    }
}
