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
        public VerificationCodeService(IVerificationCodeRepository verificationCodeRepository)
        {
            _verificationCodeRepository = verificationCodeRepository;
        }

        public async Task<int> GenerateCode(long userId)
        {
            var code = new VerificationCode
            {
                UserId = userId,
                Code = RandomNumberGenerator.GetInt32(100_000 ,1_000_000),
                ExpirationTime = DateTime.Now.AddMinutes(2)
            };
            await _verificationCodeRepository.Insert(code);
            return code.Code;
        }

        public async Task<bool> VerifyCode(long userId, int code)
        {
            var userCodes = await _verificationCodeRepository.GetByUserId(userId);
            return userCodes.Count(c => c.Code == code && c.ExpirationTime > DateTime.Now) > 0;
        }
    }
}
