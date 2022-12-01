using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Repositories;

namespace SlothOrganizer.Persistence.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly DapperContext _context;

        public RefreshTokenRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RefreshToken>> GetByUserId(long userId)
        {
            var query = "SELECT * FROM RefreshTokens WHERE UserId=@UserId";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<RefreshToken>(query, new { userId });
        }

        public async Task<RefreshToken> Insert(RefreshToken refreshToken)
        {
            var query = "INSERT INTO RefreshTokens(UserId, Token, ExpirationTime) VALUES (@UserId, @Token, @ExpirationTime)" +
                " SELECT CAST(SCOPE_IDENTITY() as bigint)";

            var parameters = new
            {
                UserId = refreshToken.UserId,
                Token = refreshToken.Token,
                ExpirationTime = refreshToken.ExpirationTime
            };

            using var connection = _context.CreateConnection();
            var id = await connection.QuerySingleAsync<long>(query, parameters);
            refreshToken.Id = id;
            return refreshToken;
        }
    }
}
