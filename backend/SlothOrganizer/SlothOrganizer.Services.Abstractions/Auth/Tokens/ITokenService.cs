using SlothOrganizer.Contracts.DTO.Auth;

namespace SlothOrganizer.Services.Abstractions.Auth.Tokens
{
    public interface ITokenService
    {
        Task<TokenDto> Generate(string email);
        string GetEmail(TokenDto token);
        Task<bool> Validate(string email, TokenDto token);
    }
}
