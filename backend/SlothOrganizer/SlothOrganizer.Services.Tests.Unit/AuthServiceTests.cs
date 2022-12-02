using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Services.Abstractions.Auth;
using SlothOrganizer.Services.Abstractions.Email;
using SlothOrganizer.Services.Abstractions.Users;
using SlothOrganizer.Services.Auth;
using Xunit;

namespace SlothOrganizer.Services.Tests.Unit
{
    public class AuthServiceTests
    {
        private readonly AuthService _sut;
        private readonly IUserService _userService;
        private readonly IAccessTokenService _accessTokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IVerificationCodeService _verificationCodeService;
        private readonly IEmailService _emailService;

        public AuthServiceTests()
        {
            _userService = A.Fake<IUserService>();
            _accessTokenService = A.Fake<IAccessTokenService>();
            _refreshTokenService = A.Fake<IRefreshTokenService>();
            _verificationCodeService = A.Fake<IVerificationCodeService>();
            _emailService = A.Fake<IEmailService>();

            _sut = new AuthService(_accessTokenService,
                _userService,
                _verificationCodeService,
                _emailService,
                _refreshTokenService);
        }

        [Fact]
        public async Task VerifyEmail_WhenValid_ShouldVerify()
        {
            // Arrange
            var dto = new VerificationCodeDto
            {
                UserId = 1,
                VerificationCode = 111111
            };
            A.CallTo(() => _userService.GetUser(1)).Returns(Task.FromResult(new UserDto { Id = 1, Email = "test"}));
            A.CallTo(() => _verificationCodeService.VerifyCode(1, 111111)).Returns(true);
            A.CallTo(() => _accessTokenService.GenerateToken("test")).Returns("test");
            A.CallTo(() => _refreshTokenService.GenerateRefreshToken(1)).Returns("test");

            // Act
            var result = await _sut.VerifyEmail(dto);

            // Assert
            Assert.Equal("test", result.AccessToken);
            A.CallTo(() => _userService.VerifyEmail(1)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task VerifyEmail_WhenInvalid_ShouldThrow()
        {
            // Arrange
            var dto = new VerificationCodeDto
            {
                UserId = 1,
                VerificationCode = 111111
            };
            A.CallTo(() => _userService.GetUser(1)).Returns(Task.FromResult(new UserDto { Id = 1, Email = "test" }));
            A.CallTo(() => _verificationCodeService.VerifyCode(1, 111111)).Returns(false);
            A.CallTo(() => _accessTokenService.GenerateToken("test")).Returns("test");
            A.CallTo(() => _refreshTokenService.GenerateRefreshToken(1)).Returns("test");

            // Act
            var code = async () => await _sut.VerifyEmail(dto);

            // Assert
            await Assert.ThrowsAsync<InvalidCredentialsException>(code);
        }

        [Fact]
        public async Task SignUp_ShouldCreateAndSendEmail()
        {
            // Arrange
            var newUser = new NewUserDto
            {
                FirstName = "test",
                LastName = "user",
                Email = "test@test.com",
                Password = "password"
            };
            A.CallTo(() => _userService.CreateUser(newUser)).Returns(Task.FromResult(new UserDto { Id = 1, Email = "test@test.com" }));
            A.CallTo(() => _verificationCodeService.GenerateCode(1)).Returns(111111);

            // Act
            await _sut.SignUp(newUser);

            // Assert
            A.CallTo(() => _userService.CreateUser(newUser)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _emailService.SendEmail("test@test.com", "Verify your email", "Your verification code is 111111"))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task RefreshToken_WhenTokenValid_ShouldRefresh()
        {
            //Arrange
            var token = new TokenDto
            {
                AccessToken = "expired",
                RefreshToken = "notExpired"
            };
            var email = "test";
            var user = new UserDto
            {
                Email = email,
                Id = 1
            };
            var accessToken = "access";
            var refreshToken = "refresh";
            A.CallTo(() => _accessTokenService.GetEmailFromToken(token.AccessToken)).Returns(email);
            A.CallTo(() => _userService.GetByEmail(email)).Returns(user);
            A.CallTo(() => _refreshTokenService.ValidateRefreshToken(user.Id, token.RefreshToken)).Returns(true);
            A.CallTo(() => _accessTokenService.GenerateToken(email)).Returns(accessToken);
            A.CallTo(() => _refreshTokenService.GenerateRefreshToken(user.Id)).Returns(refreshToken);

            //Act
            var result = await _sut.RefreshToken(token);

            //Assert
            Assert.Equal(accessToken, result.AccessToken);
            Assert.Equal(refreshToken, result.RefreshToken);
        }

        [Fact]
        public async Task RefreshToken_WhenTokenInvalid_ShouldThrow()
        {
            //Arrange
            var token = new TokenDto
            {
                AccessToken = "expired",
                RefreshToken = "notExpired"
            };
            var email = "test";
            var user = new UserDto
            {
                Email = email,
                Id = 1
            };
            A.CallTo(() => _accessTokenService.GetEmailFromToken(token.AccessToken)).Returns(email);
            A.CallTo(() => _userService.GetByEmail(email)).Returns(user);
            A.CallTo(() => _refreshTokenService.ValidateRefreshToken(user.Id, token.RefreshToken)).Returns(false);

            //Act
            var code = async () => await _sut.RefreshToken(token);

            //Assert
            await Assert.ThrowsAsync<InvalidCredentialsException>(code);
        }

        [Fact]
        public async Task SignIn_WhenEmailVerified_ShouldReturnWithToken()
        {
            //Arrange
            var auth = new AuthorizationDto
            {
                Email = "test",
                Password = "password"
            };
            var token = new TokenDto
            {
                AccessToken = "expired",
                RefreshToken = "notExpired"
            };
            var user = new UserDto
            {
                Email = auth.Email,
                Id = 1,
                EmailVerified = true
            };
            var accessToken = "access";
            var refreshToken = "refresh";
            A.CallTo(() => _userService.Authorize(auth)).Returns(user);
            A.CallTo(() => _accessTokenService.GenerateToken(user.Email)).Returns(accessToken);
            A.CallTo(() => _refreshTokenService.GenerateRefreshToken(user.Id)).Returns(refreshToken);

            //Act
            var result = await _sut.SignIn(auth);

            //Assert
            Assert.NotNull(result.Token);
            Assert.Equal(user, result.User);
            Assert.Equal(accessToken, result.Token!.AccessToken);
            Assert.Equal(refreshToken, result.Token.RefreshToken);
        }

        [Fact]
        public async Task SignIn_WhenEmailNotVerified_ShouldReturnWithoutToken()
        {
            //Arrange
            var auth = new AuthorizationDto
            {
                Email = "test",
                Password = "password"
            };
            var token = new TokenDto
            {
                AccessToken = "expired",
                RefreshToken = "notExpired"
            };
            var user = new UserDto
            {
                Email = auth.Email,
                Id = 1,
                EmailVerified = false
            };
            A.CallTo(() => _userService.Authorize(auth)).Returns(user);

            //Act
            var result = await _sut.SignIn(auth);

            //Assert
            Assert.Null(result.Token);
            Assert.Equal(user, result.User);
        }
    }
}
