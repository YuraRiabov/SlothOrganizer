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
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Services.Abstractions;
using Xunit;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace SlothOrganizer.Services.Tests.Unit
{
    public class TokenServiceTests
    {
        private readonly TokenService _sut;
        private readonly IConfiguration _configuration;
        private readonly ISecurityService _securityService;
        private readonly IDateTimeService _dateTimeService;

        public TokenServiceTests()
        {
            var inMemorySettings = new Dictionary<string, string?>
            {
                { "JwtKey", "SecretKeyOfRightLength" },
                { "Jwt:Issuer", "tests" },
                { "Jwt:Audience", "tests" }
            };
            _configuration = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();

            _securityService = A.Fake<ISecurityService>();
            _dateTimeService = A.Fake<IDateTimeService>();

            _sut = new TokenService(_configuration, _securityService, _dateTimeService);
        }

        [Fact]
        public void GenerateToken_ShouldGenerateValidToken()
        {
            // Arrange
            var bytes = new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            var email = "test@test.com";
            A.CallTo(() => _securityService.GetRandomBytes(16)).Returns(bytes);
            A.CallTo(() => _dateTimeService.Now()).Returns(new DateTime(1800, 1, 1));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = "tests",
                ValidIssuer = "tests",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretKeyOfRightLength")),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            // Act
            var token = _sut.GenerateToken(email);
            var principal = tokenHandler.ValidateToken(token.AccessToken, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            //Assert

            Assert.NotNull(jwtSecurityToken);
            Assert.Equal(Convert.ToBase64String(bytes), token.RefreshToken);
            Assert.Equal(email, principal.FindFirst(ClaimTypes.Email)?.Value);
        }

        [Fact]
        public void GenerateToken_WhenExpired_ShouldThrow()
        {
            // Arrange
            var bytes = new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            var email = "test@test.com";
            A.CallTo(() => _securityService.GetRandomBytes(16)).Returns(bytes);
            A.CallTo(() => _dateTimeService.Now()).Returns(new DateTime(1800, 1, 1));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = "tests",
                ValidIssuer = "tests",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretKeyOfRightLength")),
                ValidateLifetime = true
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            // Act
            var token = _sut.GenerateToken(email);
            var code = () => tokenHandler.ValidateToken(token.AccessToken, tokenValidationParameters, out securityToken);

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
            var bytes = new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            var email = "test@test.com";
            A.CallTo(() => _securityService.GetRandomBytes(16)).Returns(bytes);
            A.CallTo(() => _dateTimeService.Now()).Returns(new DateTime(1800, 1, 1));
            var token = _sut.GenerateToken(email).AccessToken;

            // Act
            var result = _sut.GetEmailFromToken(token);

            //Assert
            Assert.Equal(email, result);
        }
    }
}
