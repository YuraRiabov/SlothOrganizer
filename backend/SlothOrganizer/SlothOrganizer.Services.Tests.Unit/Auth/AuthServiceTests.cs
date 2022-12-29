using FakeItEasy;
using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Services.Abstractions.Auth.Tokens;
using SlothOrganizer.Services.Abstractions.Auth.UserVerification;
using SlothOrganizer.Services.Abstractions.Email;
using SlothOrganizer.Services.Abstractions.Users;
using SlothOrganizer.Services.Auth;
using Xunit;

namespace SlothOrganizer.Services.Tests.Unit.Auth
{
    public class AuthServiceTests
    {
        private readonly AuthService _authService;
        private readonly IUserService _userService;
        private readonly IAccessTokenService _accessTokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IUserVerificationService _userVerificationService;

        public AuthServiceTests()
        {
            _userService = A.Fake<IUserService>();
            _accessTokenService = A.Fake<IAccessTokenService>();
            _refreshTokenService = A.Fake<IRefreshTokenService>();
            _userVerificationService = A.Fake<IUserVerificationService>();

            _authService = new AuthService(_accessTokenService,
                _userService,
                _userVerificationService,
                _refreshTokenService);
        }

        [Fact]
        public async Task VerifyEmail_WhenValid_ShouldVerify()
        {
            var dto = GetVerificationCodeDto();
            var user = GetUser(dto.Email, true);
            A.CallTo(() => _userService.VerifyEmail(dto.Email, 111111)).Returns(user);
            A.CallTo(() => _accessTokenService.Generate(dto.Email)).Returns("test");

            var result = await _authService.VerifyEmail(dto);

            Assert.Equal("test", result.Token.AccessToken);
            A.CallTo(() => _userService.VerifyEmail(dto.Email, 111111)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task VerifyEmail_WhenInvalid_ShouldThrow()
        {
            var dto = GetVerificationCodeDto();
            A.CallTo(() => _userService.VerifyEmail(dto.Email, 111111)).Returns(Task.FromResult<UserDto?>(null));

            var code = async () => await _authService.VerifyEmail(dto);

            var exception = await Assert.ThrowsAsync<InvalidCredentialsException>(code);
            Assert.Equal("Invalid verification code", exception.Message);
        }

        [Fact]
        public async Task SignUp_ShouldCreateAndSendEmail()
        {
            var newUser = new NewUserDto
            {
                FirstName = "test",
                LastName = "user",
                Email = "test@test.com",
                Password = "password"
            };
            A.CallTo(() => _userService.Create(newUser)).Returns(Task.FromResult(new UserDto { Id = 1, Email = "test@test.com" }));

            await _authService.SignUp(newUser);

            A.CallTo(() => _userService.Create(newUser)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _userVerificationService.SendVerificationCode(newUser.Email))
                .MustHaveHappenedOnceExactly();
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
            A.CallTo(() => _userService.Get(user.Email)).Returns(user);

            await _authService.ResendVerificationCode(user.Email);

            A.CallTo(() => _userService.Get(user.Email)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _userVerificationService.SendVerificationCode(user.Email))
                .MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async Task RefreshToken_WhenTokenValid_ShouldRefresh()
        {
            var token = GetTokenDto();
            var email = "test";
            var accessToken = "access";
            var refreshToken = "refresh";
            A.CallTo(() => _accessTokenService.GetEmailFromToken(token.AccessToken)).Returns(email);
            A.CallTo(() => _refreshTokenService.Validate(email, token.RefreshToken)).Returns(true);
            A.CallTo(() => _accessTokenService.Generate(email)).Returns(accessToken);
            A.CallTo(() => _refreshTokenService.Generate(email)).Returns(refreshToken);

            var result = await _authService.RefreshToken(token);

            Assert.Equal(accessToken, result.AccessToken);
            Assert.Equal(refreshToken, result.RefreshToken);
        }

        [Fact]
        public async Task RefreshToken_WhenTokenInvalid_ShouldThrow()
        {
            var token = GetTokenDto();
            var email = "test";
            A.CallTo(() => _accessTokenService.GetEmailFromToken(token.AccessToken)).Returns(email);
            A.CallTo(() => _refreshTokenService.Validate(email, token.RefreshToken)).Returns(false);

            var code = async () => await _authService.RefreshToken(token);

            var exception = await Assert.ThrowsAsync<InvalidCredentialsException>(code);
            Assert.Equal("Invalid refresh token", exception.Message);
        }

        [Fact]
        public async Task SignIn_WhenEmailVerified_ShouldReturnWithToken()
        {
            LoginDto login = GetLoginDto();
            var token = GetTokenDto();
            var user = GetUser(login.Email, true);
            var accessToken = "access";
            var refreshToken = "refresh";
            A.CallTo(() => _userService.Get(login)).Returns(user);
            A.CallTo(() => _accessTokenService.Generate(user.Email)).Returns(accessToken);
            A.CallTo(() => _refreshTokenService.Generate(user.Email)).Returns(refreshToken);

            var result = await _authService.SignIn(login);

            Assert.NotNull(result.Token);
            Assert.Equal(user, result.User);
            Assert.Equal(accessToken, result.Token!.AccessToken);
            Assert.Equal(refreshToken, result.Token.RefreshToken);
        }

        [Fact]
        public async Task SignIn_WhenEmailNotVerified_ShouldReturnWithoutToken()
        {
            var auth = GetLoginDto();
            var user = GetUser(auth.Email, false);
            A.CallTo(() => _userService.Get(auth)).Returns(user);

            var result = await _authService.SignIn(auth);

            Assert.Null(result.Token);
            Assert.Equal(user, result.User);
        }
        private VerificationCodeDto GetVerificationCodeDto()
        {
            return new VerificationCodeDto
            {
                Email = "test@test.com",
                VerificationCode = 111111
            };
        }

        private static UserDto GetUser(string email, bool emailVerified)
        {
            return new UserDto
            {
                Email = email,
                Id = 1,
                EmailVerified = emailVerified
            };
        }

        private static LoginDto GetLoginDto()
        {
            return new LoginDto
            {
                Email = "test",
                Password = "password"
            };
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
