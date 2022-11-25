using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothOrganizer.Services.Abstractions
{
    public interface IVerificationCodeService
    {
        Task<int> GenerateCode(long userId);

        Task<bool> VerifyCode(long userId, int code);
    }
}
