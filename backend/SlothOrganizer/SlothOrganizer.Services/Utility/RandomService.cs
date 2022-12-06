using System.Security.Cryptography;
using SlothOrganizer.Services.Abstractions.Utility;

namespace SlothOrganizer.Services.Utility
{
    public class RandomService : IRandomService
    {
        public byte[] GetRandomBytes(int length = 16)
        {
            return RandomNumberGenerator.GetBytes(length);
        }

        public int GetRandomNumber(int digitCount = 6)
        {
            return RandomNumberGenerator.GetInt32((int)Math.Pow(10, digitCount - 1), (int)Math.Pow(10, digitCount));
        }
    }
}
