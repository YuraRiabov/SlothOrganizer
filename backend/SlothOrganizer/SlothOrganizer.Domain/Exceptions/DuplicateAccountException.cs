using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothOrganizer.Domain.Exceptions
{
    public class DuplicateAccountException : Exception
    {
        public DuplicateAccountException(string message) : base(message)
        {
            
        }
    }
}
