using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Services.Abstractions.Auth.Tokens;

namespace SlothOrganizer.Services.Auth.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly IAccessTokenService _accessTokenService;
        private readonly IRefreshTokenService _refreshTokenService;

        public TokenService(IRefreshTokenService refreshTokenService, IAccessTokenService accessTokenService)
        {
            _refreshTokenService = refreshTokenService;
            _accessTokenService = accessTokenService;
        }

        public async Task<TokenDto> Generate(string email, long id)
        {
            return new TokenDto
            {
                AccessToken = _accessTokenService.Generate(email, id),
                RefreshToken = await _refreshTokenService.Generate(email)
            };
        }

        public string GetEmail(TokenDto token)
        {
            return _accessTokenService.GetEmail(token.AccessToken);
        }

        public long GetId(TokenDto token)
        {
            return _accessTokenService.GetId(token.AccessToken);
        }

        public async Task<bool> Validate(string email, TokenDto token)
        {
            return await _refreshTokenService.Validate(email, token.RefreshToken);
        }
    }
}
