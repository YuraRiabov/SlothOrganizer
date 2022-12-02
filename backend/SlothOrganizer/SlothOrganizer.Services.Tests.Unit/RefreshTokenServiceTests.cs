using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Utility;
using SlothOrganizer.Services.Auth;
using Xunit;

namespace SlothOrganizer.Services.Tests.Unit
{
    public class RefreshTokenServiceTests
    {
        private readonly RefreshTokenService _sut;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ISecurityService _securityService;
        private readonly IDateTimeService _dateTimeService;

        public RefreshTokenServiceTests()
        {
            _refreshTokenRepository = A.Fake<IRefreshTokenRepository>();
            _securityService = A.Fake<ISecurityService>();
            _dateTimeService = A.Fake<IDateTimeService>();

            _sut = new RefreshTokenService(_dateTimeService, _securityService, _refreshTokenRepository);
        }

        [Fact]
        public async Task GenerateRefreshToken_ShouldGenerate()
        {
            //Arrange
            var userId = 1;
            var token = new RefreshToken
            {
                UserId = userId,
                Token = "test",
                ExpirationTime = new DateTime(2022, 12, 2)
            };
            A.CallTo(() => _securityService.GetRandomBytes(16)).Returns(Convert.FromBase64String(token.Token));
            A.CallTo(() => _dateTimeService.Now()).Returns(token.ExpirationTime);

            //Act
            var result = await _sut.GenerateRefreshToken(userId);

            //Assert
            Assert.Equal(token.Token, result);
            A.CallTo(() => _refreshTokenRepository.Insert(A<RefreshToken>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ValidateRefreshToken_WhenIsValid_ShouldBeTrue()
        {
            // Arrange
            var token = "test";
            var userId = 1;
            var tokens = new List<RefreshToken>()
            {
                new()
                {
                    Token = token,
                    UserId = userId,
                    ExpirationTime = new DateTime(2022, 11, 29, 11, 59, 0)
                },
                new()
                {
                    Token = token,
                    UserId = userId,
                    ExpirationTime = new DateTime(2022, 11, 29, 12, 1, 0)
                },
                new()
                {
                    Token = token + "1",
                    UserId = userId,
                    ExpirationTime = new DateTime(2022, 11, 30, 11, 59, 0)
                }
            };
            A.CallTo(() => _dateTimeService.Now()).Returns(new DateTime(2022, 11, 29, 12, 0, 0));
            A.CallTo(() => _refreshTokenRepository.GetByUserId(userId)).Returns(tokens);

            //Act
            var result = await _sut.ValidateRefreshToken(userId, token);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ValidateRefreshToken_WhenExpired_ShouldBeFalse()
        {
            // Arrange
            var token = "test";
            var userId = 1;
            var tokens = new List<RefreshToken>()
            {
                new()
                {
                    Token = token,
                    UserId = userId,
                    ExpirationTime = new DateTime(2022, 11, 29, 11, 59, 0)
                },
                new()
                {
                    Token = token,
                    UserId = userId,
                    ExpirationTime = new DateTime(2022, 11, 29, 12, 1, 0)
                },
                new()
                {
                    Token = token + "1",
                    UserId = userId,
                    ExpirationTime = new DateTime(2022, 11, 30, 11, 59, 0)
                }
            };
            A.CallTo(() => _dateTimeService.Now()).Returns(new DateTime(2022, 11, 30, 12, 0, 0));
            A.CallTo(() => _refreshTokenRepository.GetByUserId(userId)).Returns(tokens);

            //Act
            var result = await _sut.ValidateRefreshToken(userId, token);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task ValidateRefreshToken_WhenIsNotEqual_ShouldBeFalse()
        {
            // Arrange
            var token = "test";
            var userId = 1;
            var tokens = new List<RefreshToken>()
            {
                new()
                {
                    Token = token,
                    UserId = userId,
                    ExpirationTime = new DateTime(2022, 11, 29, 11, 59, 0)
                },
                new()
                {
                    Token = token,
                    UserId = userId,
                    ExpirationTime = new DateTime(2022, 11, 29, 12, 1, 0)
                },
                new()
                {
                    Token = token + "1",
                    UserId = userId,
                    ExpirationTime = new DateTime(2022, 11, 30, 11, 59, 0)
                }
            };
            A.CallTo(() => _dateTimeService.Now()).Returns(new DateTime(2022, 11, 29, 12, 0, 0));
            A.CallTo(() => _refreshTokenRepository.GetByUserId(userId)).Returns(tokens);

            //Act
            var result = await _sut.ValidateRefreshToken(userId, token + "2");

            // Assert
            Assert.False(result);
        }
    }
}

