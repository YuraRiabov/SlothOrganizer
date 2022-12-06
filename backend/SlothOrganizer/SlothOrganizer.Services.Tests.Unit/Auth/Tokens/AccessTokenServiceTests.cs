using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Configuration;
using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Services.Abstractions.Utility;
using SlothOrganizer.Services.Auth;
using SlothOrganizer.Services.Auth.Tokens;
using SlothOrganizer.Services.Auth.Tokens.Options;
using SlothOrganizer.Services.Utility;
using Xunit;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace SlothOrganizer.Services.Tests.Unit.Auth.Tokens
{
    public class AccessTokenServiceTests
    {
        private readonly AccessTokenService _sut;
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

            _sut = new AccessTokenService(options, _dateTimeService);
        }

        [Fact]
        public void GenerateToken_ShouldGenerateValidToken()
        {
            // Arrange=
            var email = "test@test.com";
            A.CallTo(() => _dateTimeService.Now()).Returns(new DateTime(1800, 1, 1));

            var tokenValidationParameters = GetTokenParameters(false);
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            // Act
            var token = _sut.GenerateToken(email);
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            //Assert

            Assert.NotNull(jwtSecurityToken);
            Assert.Equal(email, principal.FindFirst(ClaimTypes.Email)?.Value);
        }

        [Fact]
        public void GenerateToken_WhenExpired_ShouldThrow()
        {
            // Arrange
            var email = "test@test.com";
            A.CallTo(() => _dateTimeService.Now()).Returns(new DateTime(1800, 1, 1));

            var tokenValidationParameters = GetTokenParameters(true);
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            // Act
            var token = _sut.GenerateToken(email);
            var code = () => tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            //Assert
            Assert.Throws<SecurityTokenExpiredException>(code);
        }

        [Fact]
        public void GetEmailFromToken_WhenInvalid_ShouldThrow()
        {
            // Arrange
            var token = "invalidToken";

            // Act
            var code = () => _sut.GetEmailFromToken(token);

            //Assert
            Assert.Throws<InvalidCredentialsException>(code);
        }

        [Fact]
        public void GetEmailFromToken_WhenValid_ShouldGet()
        {
            // Arrange
            var email = "test@test.com";
            A.CallTo(() => _dateTimeService.Now()).Returns(new DateTime(1800, 1, 1));
            var token = _sut.GenerateToken(email);

            // Act
            var result = _sut.GetEmailFromToken(token);

            //Assert
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
