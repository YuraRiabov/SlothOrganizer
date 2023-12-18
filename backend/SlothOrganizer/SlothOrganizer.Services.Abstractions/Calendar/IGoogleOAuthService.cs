using SlothOrganizer.Contracts.DTO.Calendar;

namespace SlothOrganizer.Services.Abstractions.Calendar;

public interface IGoogleOAuthService
{
    string GetAuthorizationUrl();
    string GenerateOAuthRequestUrl(string scope, string redirectUrl, string codeChallenge);
    Task<TokenResultDto> ExchangeCodeOnToken(string code, string codeVerifier, string redirectUrl);
    Task<TokenResultDto> RefreshToken(string refreshToken);
}