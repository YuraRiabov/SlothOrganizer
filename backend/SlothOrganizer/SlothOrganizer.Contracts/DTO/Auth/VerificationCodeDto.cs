using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothOrganizer.Contracts.DTO.Auth
{
    public class VerificationCodeDto
    {
        public long UserId { get; set; }
        public int VerificationCode { get; set; }
    }
}
