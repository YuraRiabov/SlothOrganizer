using SlothOrganizer.Contracts.DTO.Auth;

namespace SlothOrganizer.Services.Abstractions.Auth.Tokens
{
    public interface ITokenService
    {
        Task<TokenDto> Generate(string email, long id);
        string GetEmail(TokenDto token);
        long GetId(TokenDto token);
        Task<bool> Validate(string email, TokenDto token);
    }
}
