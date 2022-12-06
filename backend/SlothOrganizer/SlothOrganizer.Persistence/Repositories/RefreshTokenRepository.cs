using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Persistence.Properties;

namespace SlothOrganizer.Persistence.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly DapperContext _context;

        public RefreshTokenRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RefreshToken>> Get(string userEmail)
        {
            var query = Resources.GetRefreshTokenByUserId;
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<RefreshToken>(query, new { userEmail });
        }

        public async Task Insert(RefreshToken refreshToken, string userEmail)
        {
            var query = Resources.InsertRefreshToken;

            var parameters = new
            {
                UserEmail = userEmail,
                Token = refreshToken.Token,
                ExpirationTime = refreshToken.ExpirationTime
            };

            using var connection = _context.CreateConnection();
            await connection.QueryAsync<long>(query, parameters);
        }
    }
}
