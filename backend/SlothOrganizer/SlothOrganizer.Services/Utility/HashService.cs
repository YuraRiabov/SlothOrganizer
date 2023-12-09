using System.Security.Cryptography;
using System.Text;
using IdentityModel;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using SlothOrganizer.Services.Abstractions.Utility;

namespace SlothOrganizer.Services.Utility
{
    public class HashService : IHashService
    {

        public string HashPassword(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }
        
        public string HashCodeChallenge(string codeVerifier)
        {
            using var sha256 = SHA256.Create();
            var challengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
            var codeChallenge = Base64Url.Encode(challengeBytes);
            return codeChallenge;
        }

        public bool VerifyPassword(string password, byte[] salt, string hash)
        {
            return hash.Equals(HashPassword(password, salt));
        }
    }
}
