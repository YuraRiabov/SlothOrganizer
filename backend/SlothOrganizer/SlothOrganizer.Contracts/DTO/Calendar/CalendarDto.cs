namespace SlothOrganizer.Contracts.DTO.Calendar;

public class CalendarDto
{
    
    public long Id { get; set; }
    public long UserId { get; set; }
    public string ConnectedCalendar { get; set; } = string.Empty;
}