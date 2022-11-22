using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothOrganizer.Services.Abstractions
{
    public interface ISecurityService
    {
        byte[] GetRandomBytes(int length = 16);

        string HashPassword(string password, byte[] salt);
    }
}
