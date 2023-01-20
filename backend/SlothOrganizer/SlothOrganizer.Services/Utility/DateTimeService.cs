using SlothOrganizer.Services.Abstractions.Utility;

namespace SlothOrganizer.Services.Utility
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now() => DateTime.Now;
    }
}
