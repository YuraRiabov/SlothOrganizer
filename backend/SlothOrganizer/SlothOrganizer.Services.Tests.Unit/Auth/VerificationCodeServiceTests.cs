using FakeItEasy;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Utility;
using SlothOrganizer.Services.Auth.UserVerification;
using Xunit;

namespace SlothOrganizer.Services.Tests.Unit.Auth
{
    public class VerificationCodeServiceTests
    {
        private readonly VerificationCodeService _verificationCodeService;
        private readonly IVerificationCodeRepository _verificationCodeRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IRandomService _randomService;

        public VerificationCodeServiceTests()
        {
            _verificationCodeRepository = A.Fake<IVerificationCodeRepository>();
            _dateTimeService = A.Fake<IDateTimeService>();
            _randomService = A.Fake<IRandomService>();
            _verificationCodeService = new VerificationCodeService(_verificationCodeRepository, _dateTimeService, _randomService);
        }

        [Fact]
        public async Task GenerateCode_ShouldAdd()
        {
            var code = 100000;
            A.CallTo(() => _dateTimeService.Now()).Returns(new DateTime(2022, 11, 29, 12, 0, 0));
            A.CallTo(() => _randomService.GetRandomNumber(6)).Returns(code);

            var result = await _verificationCodeService.Generate("email");

            Assert.Equal(code, result);
            A.CallTo(() => _verificationCodeRepository.Insert(A<VerificationCode>._, "email")).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task VerifyCode_WhenIsValid_ShouldBeTrue()
        {
            var code = 100000;
            var userId = 1;
            var codes = GetCodes(code, userId);
            A.CallTo(() => _dateTimeService.Now()).Returns(new DateTime(2022, 11, 29, 12, 0, 0));
            A.CallTo(() => _verificationCodeRepository.Get(userId)).Returns(codes);

            var result = await _verificationCodeService.Verify(userId, code);

            Assert.True(result);
        }

        [Fact]
        public async Task VerifyCode_WhenExpired_ShouldBeFalse()
        {
            var code = 100000;
            var userId = 1;
            var codes = GetCodes(code, userId);
            A.CallTo(() => _dateTimeService.Now()).Returns(new DateTime(2022, 11, 29, 13, 0, 0));
            A.CallTo(() => _verificationCodeRepository.Get(userId)).Returns(codes);

            var result = await _verificationCodeService.Verify(userId, code);

            Assert.False(result);
        }

        [Fact]
        public async Task VerifyCode_WhenIsNoEqueal_ShouldBeFalse()
        {
            var code = 100000;
            var userId = 1;
            var codes = GetCodes(code, userId);
            A.CallTo(() => _dateTimeService.Now()).Returns(new DateTime(2022, 11, 29, 12, 0, 0));
            A.CallTo(() => _verificationCodeRepository.Get(userId)).Returns(codes);

            var result = await _verificationCodeService.Verify(userId, 5);

            Assert.False(result);
        }

        private List<VerificationCode> GetCodes(int code, int userId)
        {
            return new List<VerificationCode>()
            {
                new()
                {
                    Code = code,
                    UserId = userId,
                    ExpirationTime = new DateTime(2022, 11, 29, 11, 59, 0)
                },
                new()
                {
                    Code = code,
                    UserId = userId,
                    ExpirationTime = new DateTime(2022, 11, 29, 12, 1, 0)
                },
                new()
                {
                    Code = 3,
                    UserId = userId,
                    ExpirationTime = new DateTime(2022, 11, 30, 11, 59, 0)
                }
            };
        }
    }
}
