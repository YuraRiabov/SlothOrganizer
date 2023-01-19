using SlothOrganizer.Domain.Entities;

namespace SlothOrganizer.Domain.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<IEnumerable<RefreshToken>> Get(string userEmail);

        Task Insert(RefreshToken refreshToken, string userEmail);
    }
}
