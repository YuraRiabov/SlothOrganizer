using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Auth;
using SlothOrganizer.Services.Abstractions.Utility;

namespace SlothOrganizer.Services.Auth
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

        public async Task<int> Generate(long userId)
        {
            var code = new VerificationCode
            {
                UserId = userId,
                Code = _randomService.GetRandomNumber(),
                ExpirationTime = _dateTimeService.Now().AddMinutes(2)
            };
            await _verificationCodeRepository.Insert(code);
            return code.Code;
        }

        public async Task<bool> Verify(long userId, int code)
        {
            var userCodes = await _verificationCodeRepository.Get(userId);
            return userCodes.Count(c => c.Code == code && c.ExpirationTime > _dateTimeService.Now()) > 0;
        }
    }
}
