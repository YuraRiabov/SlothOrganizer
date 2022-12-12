using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothOrganizer.Domain.Exceptions
{
    public abstract class BaseException : Exception
    {
        public abstract int StatusCode { get; protected set; }
        protected BaseException(string message) : base(message)
        {

        }
    }
}
