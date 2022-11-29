using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlothOrganizer.Services.Abstractions.Utility;

namespace SlothOrganizer.Services.Utility
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now() => DateTime.Now;
    }
}
