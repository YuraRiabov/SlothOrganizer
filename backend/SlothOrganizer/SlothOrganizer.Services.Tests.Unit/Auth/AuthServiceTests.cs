using FakeItEasy;
using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Services.Abstractions.Auth.Tokens;
using SlothOrganizer.Services.Abstractions.Auth.UserVerification;
using SlothOrganizer.Services.Abstractions.Users;
using SlothOrganizer.Services.Auth;
using Xunit;

namespace SlothOrganizer.Services.Tests.Unit.Auth
{
    public class AuthServiceTests
    {
        private readonly AuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly INotificationService _notificationService;

        public AuthServiceTests()
        {
            _tokenService = A.Fake<ITokenService>();
            _notificationService = A.Fake<INotificationService>();

            _authService = new AuthService(_tokenService, _notificationService);
        }

        [Fact]
        public async Task ResendVerificationCode_ShouldSendEmail()
        {
            var userId = 1;
            var user = new UserDto
            {
                Id = userId,
                FirstName = "test",
                LastName = "user",
                Email = "test@test.com"
            };

            await _authService.SendVerificationCode(user.Email);

            A.CallTo(() => _notificationService.SendVerificationCode(user.Email))
                .MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async Task RefreshToken_WhenTokenValid_ShouldRefresh()
        {
            var token = GetTokenDto();
            var email = "test";
            A.CallTo(() => _tokenService.GetEmail(token)).Returns(email);
            A.CallTo(() => _tokenService.Validate(email, token)).Returns(true);
            A.CallTo(() => _tokenService.Generate(email)).Returns(token);

            var result = await _authService.RefreshToken(token);

            Assert.Equal(token.AccessToken, result.AccessToken);
            Assert.Equal(token.RefreshToken, result.RefreshToken);
        }

        [Fact]
        public async Task RefreshToken_WhenTokenInvalid_ShouldThrow()
        {
            var token = GetTokenDto();
            var email = "test";
            A.CallTo(() => _tokenService.GetEmail(token)).Returns(email);
            A.CallTo(() => _tokenService.Validate(email, token)).Returns(false);

            var code = async () => await _authService.RefreshToken(token);

            var exception = await Assert.ThrowsAsync<InvalidCredentialsException>(code);
            Assert.Equal("Invalid refresh token", exception.Message);
        }

        private static TokenDto GetTokenDto()
        {
            return new TokenDto
            {
                AccessToken = "expired",
                RefreshToken = "notExpired"
            };
        }
    }
}
