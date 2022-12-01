using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothOrganizer.Services.Abstractions.Auth
{
    public interface IRefreshTokenService
    {
        public Task<string> GenerateRefreshToken(long userId);
        public Task<bool> ValidateRefreshToken(long userId, string token);
    }
}
