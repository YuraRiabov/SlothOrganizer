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
    public class AccessTokenServiceTests
    {
        private readonly AccessTokenService _accessTokenService;
        private readonly IDateTimeService _dateTimeService;

        public AccessTokenServiceTests()
        {
            var options = Options.Create(new JwtOptions
            {
                Secret = "SecretKeyOfRightLength",
                Issuer = "tests",
                Audience = "tests"
            });

            _dateTimeService = A.Fake<IDateTimeService>();

            _accessTokenService = new AccessTokenService(options, _dateTimeService);
        }

        [Fact]
        public void GenerateToken_ShouldGenerateValidToken()
        {
            var email = "test@test.com";
            var id = 1;
            A.CallTo(() => _dateTimeService.Now()).Returns(new DateTime(1800, 1, 1));

            var tokenValidationParameters = GetTokenParameters(false);
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var token = _accessTokenService.Generate(email, id);
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            Assert.NotNull(jwtSecurityToken);
            Assert.Equal(email, principal.FindFirst(ClaimTypes.Email)?.Value);
            Assert.Equal(id.ToString(), principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        [Fact]
        public void GenerateToken_WhenExpired_ShouldThrow()
        {
            var email = "test@test.com";
            var id = 1;
            A.CallTo(() => _dateTimeService.Now()).Returns(new DateTime(1800, 1, 1));

            var tokenValidationParameters = GetTokenParameters(true);
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var token = _accessTokenService.Generate(email, id);
            var code = () => tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            Assert.Throws<SecurityTokenExpiredException>(code);
        }

        [Fact]
        public void GetEmailFromToken_WhenInvalid_ShouldThrow()
        {
            var token = "invalidToken";

            var code = () => _accessTokenService.GetEmail(token);

            var exception = Assert.Throws<InvalidCredentialsException>(code);
            Assert.Equal("Invalid token", exception.Message);
        }

        [Fact]
        public void GetEmailFromToken_WhenValid_ShouldGet()
        {
            var email = "test@test.com";
            var id = 1;
            A.CallTo(() => _dateTimeService.Now()).Returns(new DateTime(1800, 1, 1));
            var token = _accessTokenService.Generate(email, id);

            var result = _accessTokenService.GetEmail(token);

            Assert.Equal(email, result);
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
