using SlothOrganizer.Services.Abstractions.Utility;
using SlothOrganizer.Services.Utility;
using Xunit;

namespace SlothOrganizer.Services.Tests.Unit.Utility
{
    public class RandomServiceTests
    {
        private readonly IRandomService _randomService;
        public RandomServiceTests()
        {
            _randomService = new RandomService();
        }

        [Fact]
        public void GenerateRandomNumber_ShouldGenerateCorrectLength()
        {
            var firstLength = 4;
            var secondLength = 6;
            var thirdLength = 8;

            var firstNumber = _randomService.GetRandomNumber(firstLength);
            var secondNumber = _randomService.GetRandomNumber(secondLength);
            var thirdNumber = _randomService.GetRandomNumber(thirdLength);

            Assert.Equal(firstLength, Math.Floor(Math.Log10(firstNumber)) + 1);
            Assert.Equal(secondLength, Math.Floor(Math.Log10(secondNumber)) + 1);
            Assert.Equal(thirdLength, Math.Floor(Math.Log10(thirdNumber)) + 1);
        }
    }
}
