using FakeItEasy;
using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Services.Abstractions.Auth;
using SlothOrganizer.Services.Abstractions.Auth.Tokens;
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
        private readonly ITokenService _tokenService;
        private readonly IVerificationCodeService _verificationCodeService;
        private readonly IEmailService _emailService;

        public AuthServiceTests()
        {
            _userService = A.Fake<IUserService>();
            _tokenService = A.Fake<ITokenService>();
            _verificationCodeService = A.Fake<IVerificationCodeService>();
            _emailService = A.Fake<IEmailService>();

            _authService = new AuthService(_tokenService, _userService, _verificationCodeService, _emailService);
        }

        [Fact]
        public async Task VerifyEmail_WhenValid_ShouldVerify()
        {
            var dto = GetVerificationCodeDto();
            A.CallTo(() => _userService.Get(1)).Returns(Task.FromResult(new UserDto { Id = 1, Email = "test" }));
            A.CallTo(() => _verificationCodeService.Verify(1, 111111)).Returns(true);
            A.CallTo(() => _tokenService.GenerateToken("test")).Returns(new TokenDto { AccessToken = "test" });

            var result = await _authService.VerifyEmail(dto);

            Assert.Equal("test", result.AccessToken);
            A.CallTo(() => _userService.VerifyEmail(1)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task VerifyEmail_WhenInvalid_ShouldThrow()
        {
            var dto = GetVerificationCodeDto();
            A.CallTo(() => _userService.Get(1)).Returns(Task.FromResult(new UserDto { Id = 1, Email = "test" }));
            A.CallTo(() => _verificationCodeService.Verify(1, 111111)).Returns(false);

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
            A.CallTo(() => _verificationCodeService.Generate(1)).Returns(111111);

            await _authService.SignUp(newUser);

            A.CallTo(() => _userService.Create(newUser)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _emailService.SendEmail("test@test.com", "Verify your email", "Your verification code is 111111"))
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
            A.CallTo(() => _userService.Get(userId)).Returns(user);
            A.CallTo(() => _verificationCodeService.Generate(userId)).Returns(111111);

            await _authService.ResendVerificationCode(userId);

            A.CallTo(() => _userService.Get(userId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _emailService.SendEmail("test@test.com", "Verify your email", "Your verification code is 111111"))
                .MustHaveHappenedOnceExactly();
        }
        private VerificationCodeDto GetVerificationCodeDto()
        {
            return new VerificationCodeDto
            {
                UserId = 1,
                VerificationCode = 111111
            };
        }
    }
}
