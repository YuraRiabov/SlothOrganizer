using SlothOrganizer.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace SlothOrganizer.Domain.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<IEnumerable<RefreshToken>> Get(string userEmail);

        Task Insert(RefreshToken refreshToken, string userEmail);
    }
}
