using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlothOrganizer.Domain.Entities;

namespace SlothOrganizer.Domain.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<IEnumerable<RefreshToken>> GetByUserId(long userId);

        Task<RefreshToken> Insert(RefreshToken refreshToken);
    }
}
