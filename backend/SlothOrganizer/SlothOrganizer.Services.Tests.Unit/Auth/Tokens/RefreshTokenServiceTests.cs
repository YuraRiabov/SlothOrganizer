using FakeItEasy;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Utility;
using SlothOrganizer.Services.Auth.Tokens;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace SlothOrganizer.Services.Tests.Unit.Auth.Tokens
{
    public class RefreshTokenServiceTests
    {
        private readonly RefreshTokenService _refreshTokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IRandomService _randomService;
        private readonly IDateTimeService _dateTimeService;

        public string Email { get; set; } = "test@test.com";

        public RefreshTokenServiceTests()
        {
            _refreshTokenRepository = A.Fake<IRefreshTokenRepository>();
            _randomService = A.Fake<IRandomService>();
            _dateTimeService = A.Fake<IDateTimeService>();

            _refreshTokenService = new RefreshTokenService(_dateTimeService, _randomService, _refreshTokenRepository);
        }

        [Fact]
        public async Task GenerateRefreshToken_ShouldGenerate()
        {
            var token = new RefreshToken
            {
                UserId = 1,
                Token = "test",
                ExpirationTime = new DateTime(2022, 12, 2)
            };
            A.CallTo(() => _randomService.GetRandomBytes(16)).Returns(Convert.FromBase64String(token.Token));
            A.CallTo(() => _dateTimeService.Now()).Returns(token.ExpirationTime);

            var result = await _refreshTokenService.Generate(Email);

            Assert.Equal(token.Token, result);
            A.CallTo(() => _refreshTokenRepository.Insert(A<RefreshToken>._, Email)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ValidateRefreshToken_WhenIsValid_ShouldBeTrue()
        {
            var token = "test";
            var tokens = GetTokens(token);
            A.CallTo(() => _dateTimeService.Now()).Returns(new DateTime(2022, 11, 29, 12, 0, 0));
            A.CallTo(() => _refreshTokenRepository.Get(Email)).Returns(tokens);

            var result = await _refreshTokenService.Validate(Email, token);

            Assert.True(result);
        }

        [Fact]
        public async Task ValidateRefreshToken_WhenExpired_ShouldBeFalse()
        {
            var token = "test";
            var tokens = GetTokens(token);
            A.CallTo(() => _dateTimeService.Now()).Returns(new DateTime(2022, 11, 30, 12, 0, 0));
            A.CallTo(() => _refreshTokenRepository.Get(Email)).Returns(tokens);

            var result = await _refreshTokenService.Validate(Email, token);

            Assert.False(result);
        }

        [Fact]
        public async Task ValidateRefreshToken_WhenIsNotEqual_ShouldBeFalse()
        {
            var token = "test";
            var tokens = GetTokens(token);
            A.CallTo(() => _dateTimeService.Now()).Returns(new DateTime(2022, 11, 29, 12, 0, 0));
            A.CallTo(() => _refreshTokenRepository.Get(Email)).Returns(tokens);

            var result = await _refreshTokenService.Validate(Email, token + "2");

            Assert.False(result);
        }

        private static List<RefreshToken> GetTokens(string token)
        {
            return new List<RefreshToken>()
            {
                new()
                {
                    Token = token,
                    UserId = 1,
                    ExpirationTime = new DateTime(2022, 11, 29, 11, 59, 0)
                },
                new()
                {
                    Token = token,
                    UserId = 1,
                    ExpirationTime = new DateTime(2022, 11, 29, 12, 1, 0)
                },
                new()
                {
                    Token = token + "1",
                    UserId = 1,
                    ExpirationTime = new DateTime(2022, 11, 30, 11, 59, 0)
                }
            };
        }
    }
}

