using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothOrganizer.Domain.Entities
{
    public class VerificationCode
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int Code { get; set; }
        public DateTime ExpirationTime { get; set; }

        public User User { get; set; }
    }
}
