using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Utility;
using SlothOrganizer.Services.Auth;
using Xunit;

namespace SlothOrganizer.Services.Tests.Unit
{
    public class VerificationCodeServiceTests
    {
        private readonly VerificationCodeService _sut;
        private readonly IVerificationCodeRepository _verificationCodeRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly ISecurityService _securityService;

        public VerificationCodeServiceTests()
        {
            _verificationCodeRepository = A.Fake<IVerificationCodeRepository>();
            _dateTimeService = A.Fake<IDateTimeService>();
            _securityService = A.Fake<ISecurityService>();
            _sut = new VerificationCodeService(_verificationCodeRepository, _dateTimeService, _securityService);
        }

        [Fact]
        public async Task GenerateCode_ShouldAdd()
        {
            // Arrange
            var code = 100000;
            A.CallTo(() => _dateTimeService.Now()).Returns(new DateTime(2022, 11, 29, 12, 0, 0));
            A.CallTo(() => _securityService.GetRandomNumber(6)).Returns(code);

            //Act
            var result = await _sut.GenerateCode(1);

            // Assert
            Assert.Equal(code, result);
            A.CallTo(() => _verificationCodeRepository.Insert(A<VerificationCode>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task VerifyCode_WhenIsValid_ShouldBeTrue()
        {
            // Arrange
            var code = 100000;
            var userId = 1;
            var codes = new List<VerificationCode>()
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
            A.CallTo(() => _dateTimeService.Now()).Returns(new DateTime(2022, 11, 29, 12, 0, 0));
            A.CallTo(() => _verificationCodeRepository.GetByUserId(userId)).Returns(codes);

            //Act
            var result = await _sut.VerifyCode(userId, code);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task VerifyCode_WhenExpired_ShouldBeFalse()
        {
            // Arrange
            var code = 100000;
            var userId = 1;
            var codes = new List<VerificationCode>()
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
                    ExpirationTime = new DateTime(2022, 11, 29, 11, 1, 0)
                },
                new()
                {
                    Code = 3,
                    UserId = userId,
                    ExpirationTime = new DateTime(2022, 11, 30, 11, 59, 0)
                }
            };
            A.CallTo(() => _dateTimeService.Now()).Returns(new DateTime(2022, 11, 29, 12, 0, 0));
            A.CallTo(() => _verificationCodeRepository.GetByUserId(userId)).Returns(codes);

            //Act
            var result = await _sut.VerifyCode(userId, code);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task VerifyCode_WhenIsNoEqueal_ShouldBeFalse()
        {
            // Arrange
            var code = 100000;
            var userId = 1;
            var codes = new List<VerificationCode>()
            {
                new()
                {
                    Code = code + 1,
                    UserId = userId,
                    ExpirationTime = new DateTime(2022, 11, 29, 11, 59, 0)
                },
                new()
                {
                    Code = code + 3,
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
            A.CallTo(() => _dateTimeService.Now()).Returns(new DateTime(2022, 11, 29, 12, 0, 0));
            A.CallTo(() => _verificationCodeRepository.GetByUserId(userId)).Returns(codes);

            //Act
            var result = await _sut.VerifyCode(userId, code);

            // Assert
            Assert.False(result);
        }
    }
}
