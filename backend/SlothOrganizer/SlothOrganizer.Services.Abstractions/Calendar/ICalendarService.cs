using SlothOrganizer.Contracts.DTO.Calendar;

namespace SlothOrganizer.Services.Abstractions.Calendar;

public interface ICalendarService
{
    Task CreateGoogleCalendarConnection(TokenResultDto tokenResultDto, long currentUserId);
    Task<CalendarDto?> Get(long userId);
    Task Delete(long calendarId);
}