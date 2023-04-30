using SlothOrganizer.Services.Abstractions.Utility;

namespace SlothOrganizer.Services.Utility
{
    public class DateTimeService : IDateTimeService
    {
        public DateTimeOffset Now() => DateTimeOffset.Now;
    }
}
