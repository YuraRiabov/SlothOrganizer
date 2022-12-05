using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FakeItEasy;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Services.Abstractions.Utility;
using SlothOrganizer.Services.Auth.Tokens;
using SlothOrganizer.Services.Auth.Tokens.Options;
using Xunit;

namespace SlothOrganizer.Services.Tests.Unit.Auth.Tokens
{
    public class TokenServiceTests
    {
        private readonly TokenService _tokenService;
        private readonly IRandomService _randomService;
        private readonly IDateTimeService _dateTimeService;

        public TokenServiceTests()
        {
            var options = Options.Create(new JwtOptions
            {
                Secret = "SecretKeyOfRightLength",
                Issuer = "tests",
                Audience = "tests"
            });

            _randomService = A.Fake<IRandomService>();
            _dateTimeService = A.Fake<IDateTimeService>();

            _tokenService = new TokenService(options, _randomService, _dateTimeService);

            A.CallTo(() => _randomService.GetRandomBytes(16)).Returns(GetBytes());
            A.CallTo(() => _dateTimeService.Now()).Returns(new DateTime(1800, 1, 1));
        }

        [Fact]
        public void GenerateToken_ShouldGenerateValidToken()
        {
            var bytes = GetBytes();
            var email = "test@test.com";

            var tokenValidationParameters = GetTokenParameters(false);
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var token = _tokenService.GenerateToken(email);
            var principal = tokenHandler.ValidateToken(token.AccessToken, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            Assert.NotNull(jwtSecurityToken);
            Assert.Equal(Convert.ToBase64String(bytes), token.RefreshToken);
            Assert.Equal(email, principal.FindFirst(ClaimTypes.Email)?.Value);
        }

        [Fact]
        public void GenerateToken_WhenExpired_ShouldThrow()
        {
            var email = "test@test.com";
            TokenValidationParameters tokenValidationParameters = GetTokenParameters(true);
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var token = _tokenService.GenerateToken(email);
            var code = () => tokenHandler.ValidateToken(token.AccessToken, tokenValidationParameters, out securityToken);

            Assert.Throws<SecurityTokenExpiredException>(code);
        }

        [Fact]
        public void GetEmailFromToken_WhenInvalid_ShouldThrow()
        {
            var token = "invalidToken";

            var code = () => _tokenService.GetEmailFromToken(token);

            var exception = Assert.Throws<InvalidCredentialsException>(code);
            Assert.Equal("Invalid token", exception.Message);
        }

        [Fact]
        public void GetEmailFromToken_WhenValid_ShouldGet()
        {
            var email = "test@test.com";
            var token = _tokenService.GenerateToken(email).AccessToken;

            var result = _tokenService.GetEmailFromToken(token);

            Assert.Equal(email, result);
        }

        private byte[] GetBytes()
        {
            return new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }

        private TokenValidationParameters GetTokenParameters(bool validateLifetime)
        {
            return new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = "tests",
                ValidIssuer = "tests",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretKeyOfRightLength")),
                ValidateLifetime = validateLifetime
            };
        }
    }
}
