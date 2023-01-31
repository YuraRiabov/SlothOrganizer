using FakeItEasy;
using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Services.Abstractions.Auth.Tokens;
using SlothOrganizer.Services.Auth.Tokens;
using Xunit;

namespace SlothOrganizer.Services.Tests.Unit.Auth.Tokens
{
    public class TokenServiceTests
    {
        private readonly IAccessTokenService _acceessTokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly TokenService _tokenService;

        public TokenServiceTests()
        {
            _acceessTokenService = A.Fake<IAccessTokenService>();
            _refreshTokenService = A.Fake<IRefreshTokenService>();

            _tokenService = new TokenService(_refreshTokenService, _acceessTokenService);
        }

        [Fact]
        public async Task Generate_ShouldGenerate()
        {
            var email = "test";
            var id = 1;
            var token = GetTokenDto();
            A.CallTo(() => _refreshTokenService.Generate(email)).Returns(token.RefreshToken);
            A.CallTo(() => _acceessTokenService.Generate(email, id)).Returns(token.AccessToken);

            var result = await _tokenService.Generate(email, id);

            Assert.Equal(result.AccessToken, token.AccessToken);
            Assert.Equal(result.RefreshToken, token.RefreshToken);
        }

        [Fact]
        public async Task Validate_ShouldValidate()
        {
            var email = "test";
            var token = GetTokenDto();
            A.CallTo(() => _refreshTokenService.Validate(email, token.RefreshToken)).Returns(true);

            var result = await _tokenService.Validate(email, token);

            Assert.True(result);
        }

        [Fact]
        public void GetEmail_ShouldGet()
        {
            var email = "test";
            var token = GetTokenDto();
            A.CallTo(() => _acceessTokenService.GetEmail(token.AccessToken)).Returns(email);

            var result = _tokenService.GetEmail(token);

            Assert.Equal(email, result);
        }

        private static TokenDto GetTokenDto()
        {
            return new TokenDto
            {
                AccessToken = "access",
                RefreshToken = "refresh"
            };
        }
    }
}
