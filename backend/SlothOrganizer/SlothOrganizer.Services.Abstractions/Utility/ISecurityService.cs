using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothOrganizer.Services.Abstractions.Utility
{
    public interface ISecurityService
    {
        byte[] GetRandomBytes(int length = 16);
        string HashPassword(string password, byte[] salt);
        bool VerifyPassword(string password, byte[] salt, string hash);
        int GetRandomNumber(int digitCount = 6);
    }
}
