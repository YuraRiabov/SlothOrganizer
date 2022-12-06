using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;

namespace SlothOrganizer.Services.Abstractions.Auth.Tokens
{
    public interface IAccessTokenService
    {
        string GenerateToken(string email);
        string GetEmailFromToken(string token);
    }
}
