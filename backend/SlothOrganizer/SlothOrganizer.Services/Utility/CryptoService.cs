using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using SlothOrganizer.Services.Abstractions.Utility;

namespace SlothOrganizer.Services.Utility
{
    public class CryptoService : ICryptoService
    {
        private readonly IConfiguration _configuration;

        public CryptoService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Encrypt(string value)
        {
            var algorithm = CreateAlgorithm();
            var toEncryptArray = Encoding.UTF8.GetBytes(value);
            var encryptor = algorithm.CreateEncryptor();
            var resultArray = encryptor.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            algorithm.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public string Decrypt(string value)
        {
            var algorithm = CreateAlgorithm();
            var toDercyptArray = Convert.FromBase64String(value);
            var decryptor = algorithm.CreateDecryptor();
            var resultArray = decryptor.TransformFinalBlock(toDercyptArray, 0, toDercyptArray.Length);            
            algorithm.Clear();
            return Encoding.UTF8.GetString(resultArray);
        }

        private Aes CreateAlgorithm()
        {
            var key = _configuration["EncryptionKey"];
            var keyArray = Encoding.UTF8.GetBytes(key);

            var algorithm = Aes.Create();
            algorithm.Key = keyArray;
            algorithm.Mode = CipherMode.ECB;
            algorithm.Padding = PaddingMode.PKCS7;
            return algorithm;
        }
    }
}
