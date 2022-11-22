using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using SlothOrganizer.Services.Abstractions;

namespace SlothOrganizer.Services
{
    public class SecurityService : ISecurityService
    {
        public byte[] GetRandomBytes(int length = 16)
        {
            return RandomNumberGenerator.GetBytes(length);
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
    }
}
