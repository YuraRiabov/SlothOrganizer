using Dapper;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Repositories;

namespace SlothOrganizer.Persistence.Repositories
{
    public class VerificationCodeRepository : IVerificationCodeRepository
    {
        private readonly DapperContext _context;

        public VerificationCodeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VerificationCode>> GetByUserId(long userId)
        {
            var query = "SELECT * FROM VerificationCodes WHERE UserId=@UserId";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<VerificationCode>(query, new { userId });
        }

        public async Task<VerificationCode> Insert(VerificationCode verificationCode)
        {
            var query = "INSERT INTO VerificationCodes(UserId, Code, ExpirationTime) VALUES (@UserId, @Code, @ExpirationTime)" +
                " SELECT CAST(SCOPE_IDENTITY() as bigint)";

            var parameters = new
            {
                UserId = verificationCode.UserId,
                Code = verificationCode.Code,
                ExpirationTime = verificationCode.ExpirationTime
            };

            using var connection = _context.CreateConnection();
            var id = await connection.QuerySingleAsync<long>(query, parameters);
            verificationCode.Id = id;
            return verificationCode;
        }
    }
}
