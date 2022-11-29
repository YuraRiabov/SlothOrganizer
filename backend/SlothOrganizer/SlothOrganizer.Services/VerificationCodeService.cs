using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions;

namespace SlothOrganizer.Services
{
    public class VerificationCodeService : IVerificationCodeService
    {
        private readonly IVerificationCodeRepository _verificationCodeRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly SecurityService _securityService;
        public VerificationCodeService(IVerificationCodeRepository verificationCodeRepository, IDateTimeService dateTimeService, SecurityService securityService)
        {
            _verificationCodeRepository = verificationCodeRepository;
            _dateTimeService = dateTimeService;
            _securityService = securityService;
        }

        public async Task<int> GenerateCode(long userId)
        {
            var code = new VerificationCode
            {
                UserId = userId,
                Code = _securityService.GetRandomNumber(),
                ExpirationTime = _dateTimeService.Now().AddMinutes(2)
            };
            await _verificationCodeRepository.Insert(code);
            return code.Code;
        }

        public async Task<bool> VerifyCode(long userId, int code)
        {
            var userCodes = await _verificationCodeRepository.GetByUserId(userId);
            return userCodes.Count(c => c.Code == code && c.ExpirationTime > _dateTimeService.Now()) > 0;
        }
    }
}
