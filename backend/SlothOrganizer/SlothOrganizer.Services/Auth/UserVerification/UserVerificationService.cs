using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Services.Abstractions.Auth.UserVerification;
using SlothOrganizer.Services.Abstractions.Email;

namespace SlothOrganizer.Services.Auth.UserVerification
{
    public class UserVerificationService : IUserVerificationService
    {
        private readonly IVerificationCodeService _verificationCodeService;
        private readonly IEmailService _emailService;

        public UserVerificationService(IVerificationCodeService verificationCodeService, IEmailService emailService)
        {
            _verificationCodeService = verificationCodeService;
            _emailService = emailService;
        }

        public async Task SendVerificationCode(string userEmail)
        {
            var code = await _verificationCodeService.Generate(userEmail);
            await _emailService.SendEmail(userEmail, "Verify your email", $"Your verification code is {code}");
        }

        public async Task SendPasswordReset(string userEmail)
        {

        }
    }
}
