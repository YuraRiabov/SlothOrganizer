using System;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Services.Abstractions.Auth.UserVerification;
using SlothOrganizer.Services.Abstractions.Email;
using SlothOrganizer.Services.Abstractions.Utility;

namespace SlothOrganizer.Services.Auth.UserVerification
{
    public class UserVerificationService : IUserVerificationService
    {
        private readonly IVerificationCodeService _verificationCodeService;
        private readonly IEmailService _emailService;
        private readonly ICryptoService _cryptoService;
        private readonly IConfiguration _configuration;

        public UserVerificationService(IVerificationCodeService verificationCodeService, IEmailService emailService, ICryptoService cryptoService, IConfiguration configuration)
        {
            _verificationCodeService = verificationCodeService;
            _emailService = emailService;
            _cryptoService = cryptoService;
            _configuration = configuration;
        }

        public async Task SendVerificationCode(string userEmail)
        {
            var code = await _verificationCodeService.Generate(userEmail);
            await _emailService.SendEmail(userEmail, "Verify your email", $"Your verification code is {code}");
        }

        public async Task SendPasswordReset(string userEmail)
        {
            var code = await _verificationCodeService.Generate(userEmail);
            var link = _configuration["ResetPasswordLink"];
            var encryptedEmail = _cryptoService.Encrypt(userEmail);
            var encryptedCode = _cryptoService.Encrypt(code.ToString());

            var param = new Dictionary<string, string>() { { "email", encryptedEmail }, { "code", encryptedCode } };

            var url = new Uri(QueryHelpers.AddQueryString(link, param));
            await _emailService.SendEmail(userEmail, "Follow the link to reset your password", $"Follow the link to reset your password: {url}");
        }
    }
}
