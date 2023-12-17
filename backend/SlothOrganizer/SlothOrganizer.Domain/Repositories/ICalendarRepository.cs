using SlothOrganizer.Domain.Entities;

namespace SlothOrganizer.Domain.Repositories;

public interface ICalendarRepository
{
    Task<Calendar> Insert(Calendar calendar);
    Task<Calendar?> Get(long userId);
    Task Delete(long calendarId);
}