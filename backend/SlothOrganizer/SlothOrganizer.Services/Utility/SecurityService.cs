using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using SlothOrganizer.Services.Abstractions.Utility;

namespace SlothOrganizer.Services.Utility
{
    public class SecurityService : ISecurityService
    {
        public byte[] GetRandomBytes(int length = 16)
        {
            return RandomNumberGenerator.GetBytes(length);
        }

        public int GetRandomNumber(int digitCount = 6)
        {
            return RandomNumberGenerator.GetInt32((int)Math.Pow(10, digitCount - 1), (int)Math.Pow(10, digitCount));
        }

        public string HashPassword(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }

        public bool VerifyPassword(string password, byte[] salt, string hash)
        {
            return HashPassword(password, salt) == hash;
        }
    }
}
