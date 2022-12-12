using SlothOrganizer.Services.Utility;
using Xunit;

namespace SlothOrganizer.Services.Tests.Unit.Utility
{
    public class HashServiceTests
    {
        private readonly HashService _hashService;

        public HashServiceTests()
        {
            _hashService = new HashService();
        }

        [Fact]
        public void HashPassword_ShouldBeConsistent()
        {
            var password = "password";
            var repeatPassword = "password";
            var anotherPassword = "Password";

            var salt = new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            var anotherSalt = new byte[] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

            var hash = _hashService.HashPassword(password, salt);
            var repeatHash = _hashService.HashPassword(repeatPassword, salt);
            var anotherPasswordHash = _hashService.HashPassword(anotherPassword, salt);
            var anotherSaltHash = _hashService.HashPassword(password, anotherSalt);

            Assert.Equal(repeatHash, hash);
            Assert.NotEqual(anotherPasswordHash, hash);
            Assert.NotEqual(anotherSaltHash, hash);
        }
    }
}
