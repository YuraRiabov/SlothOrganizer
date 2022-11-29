using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlothOrganizer.Services.Utility;
using Xunit;

namespace SlothOrganizer.Services.Tests.Unit
{
    public class SecurityServiceTests
    {
        private readonly SecurityService _sut;

        public SecurityServiceTests()
        {
            _sut = new SecurityService();
        }

        [Fact]
        public void HashPassword_ShouldBeConsistent()
        {
            // Arrange
            var password = "password";
            var repeatPassword = "password";
            var anotherPassword = "Password";

            var salt = new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            var anotherSalt = new byte[] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

            // Act
            var hash = _sut.HashPassword(password, salt);
            var repeatHash = _sut.HashPassword(repeatPassword, salt);
            var anotherPasswordHash = _sut.HashPassword(anotherPassword, salt);
            var anotherSaltHash = _sut.HashPassword(password, anotherSalt);

            // Assert
            Assert.Equal(repeatHash, hash);
            Assert.NotEqual(anotherPasswordHash, hash);
            Assert.NotEqual(anotherSaltHash, hash);
        }

        [Fact]
        public void GenerateRandomNumber_ShouldGenerateCorrectLength()
        {
            // Arrange
            var firstLength = 4;
            var secondLength = 6;
            var thirdLength = 8;

            // Act
            var firstNumber = _sut.GetRandomNumber(firstLength);
            var secondNumber = _sut.GetRandomNumber(secondLength);
            var thirdNumber = _sut.GetRandomNumber(thirdLength);

            // Assert
            Assert.Equal(firstLength, Math.Floor(Math.Log10(firstNumber)) + 1);
            Assert.Equal(secondLength, Math.Floor(Math.Log10(secondNumber)) + 1);
            Assert.Equal(thirdLength, Math.Floor(Math.Log10(thirdNumber)) + 1);
        }
    }
}
