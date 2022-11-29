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
using SlothOrganizer.Services.Abstractions;
using Xunit;

namespace SlothOrganizer.Services.Tests.Unit
{
    public class AuthServiceTests
    {
        private readonly AuthService _sut;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IVerificationCodeService _verificationCodeService;
        private readonly IEmailService _emailService;

        public AuthServiceTests()
        {
            _userService = A.Fake<IUserService>();
            _tokenService = A.Fake<ITokenService>();
            _verificationCodeService = A.Fake<IVerificationCodeService>();
            _emailService = A.Fake<IEmailService>();

            _sut = new AuthService(_tokenService, _userService, _verificationCodeService, _emailService);
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
            A.CallTo(() => _tokenService.GenerateToken("test")).Returns(new TokenDto { AccessToken = "test" });

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
            A.CallTo(() => _tokenService.GenerateToken("test")).Returns(new TokenDto { AccessToken = "test" });

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
    }
}
