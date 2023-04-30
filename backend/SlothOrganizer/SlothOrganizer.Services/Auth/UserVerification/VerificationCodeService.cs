using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Auth.UserVerification;
using SlothOrganizer.Services.Abstractions.Utility;

namespace SlothOrganizer.Services.Auth.UserVerification
{
    public class VerificationCodeService : IVerificationCodeService
    {
        private readonly IVerificationCodeRepository _verificationCodeRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IRandomService _randomService;
        public VerificationCodeService(IVerificationCodeRepository verificationCodeRepository, IDateTimeService dateTimeService, IRandomService randomService)
        {
            _verificationCodeRepository = verificationCodeRepository;
            _dateTimeService = dateTimeService;
            _randomService = randomService;
        }

        public async Task<int> Generate(string userEmail)
        {
            var code = new VerificationCode
            {
                Code = _randomService.GetRandomNumber(),
                ExpirationTime = _dateTimeService.Now().AddMinutes(2)
            };
            await _verificationCodeRepository.Insert(code, userEmail);
            return code.Code;
        }

        public async Task<bool> Verify(string userEmail, int code)
        {
            var userCodes = await _verificationCodeRepository.Get(userEmail);
            return userCodes.Count(c => c.Code == code && c.ExpirationTime > _dateTimeService.Now()) > 0;
        }
    }
}
