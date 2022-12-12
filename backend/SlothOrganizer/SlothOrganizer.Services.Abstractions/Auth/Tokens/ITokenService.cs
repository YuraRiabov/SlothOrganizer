using SlothOrganizer.Contracts.DTO.Auth;

namespace SlothOrganizer.Services.Abstractions.Auth.Tokens
{
    public interface ITokenService
    {
        TokenDto GenerateToken(string email);
        string GetEmailFromToken(string token);
    }
}
