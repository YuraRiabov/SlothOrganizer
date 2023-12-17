namespace SlothOrganizer.Contracts.DTO.Calendar;

public class CalendarEventDto
{
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
    public string Name { get; set; } = string.Empty;
}