using FakeItEasy;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using SlothOrganizer.Services.Abstractions.Auth.UserVerification;
using SlothOrganizer.Services.Abstractions.Email;
using SlothOrganizer.Services.Abstractions.Utility;
using SlothOrganizer.Services.Auth.UserVerification;
using Xunit;

namespace SlothOrganizer.Services.Tests.Unit.Auth.UserVerification
{
    public class EmailNotificationServiceTests
    {
        private readonly IVerificationCodeService _verificationCodeService;
        private readonly IEmailService _emailService;
        private readonly ICryptoService _cryptoService;
        private readonly IConfiguration _configuration;
        private readonly EmailNotificationService _userVerificationService;

        public EmailNotificationServiceTests()
        {
            var configuration = new Dictionary<string, string?> { { "ResetPasswordLink", "https://test.com" } };
            _configuration = new ConfigurationBuilder().AddInMemoryCollection(configuration).Build();
            _emailService = A.Fake<IEmailService>();
            _cryptoService = A.Fake<ICryptoService>();
            _verificationCodeService = A.Fake<IVerificationCodeService>();
            _userVerificationService = new EmailNotificationService(_verificationCodeService, _emailService, _cryptoService, _configuration);
        }

        [Fact]
        public async Task SendVerificationCode_ShouldSend()
        {
            var email = "test@test.com";
            var code = 111111;

            A.CallTo(() => _verificationCodeService.Generate(email)).Returns(code);

            await _userVerificationService.SendVerificationCode(email);

            A.CallTo(() => _emailService.SendEmail(email, "Verify your email", "Your verification code is 111111")).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task SendPasswordReset_ShouldSend()
        {
            var email = "test@test.com";
            var code = 111111;

            var link = _configuration["ResetPasswordLink"];
            var param = new Dictionary<string, string>() { { "email", email }, { "code", code.ToString() } };
            var url = new Uri(QueryHelpers.AddQueryString(link, param));

            A.CallTo(() => _verificationCodeService.Generate(email)).Returns(code);
            A.CallTo(() => _cryptoService.Encrypt(email)).Returns(email);
            A.CallTo(() => _cryptoService.Encrypt(code.ToString())).Returns(code.ToString());

            await _userVerificationService.SendPasswordResetLink(email);

            A.CallTo(() => _emailService.SendEmail(email,
                "Follow the link to reset your password",
                $"Follow the link to reset your password: {url}"))
                .MustHaveHappenedOnceExactly();
        }
    }
}
