using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;

namespace SlothOrganizer.Services.Abstractions
{
    public interface ITokenService
    {
        TokenDto GenerateToken(string email);
        string GetEmailFromExpiredToken(string token);
        string GenerateAccessToken(string email);
        string GenerateRefreshToken();
    }
}
