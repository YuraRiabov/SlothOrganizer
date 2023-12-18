namespace SlothOrganizer.Domain.Entities;

public class Calendar
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string ConnectedCalendar { get; set; } = string.Empty;
    public string RefreshToken { get; set; }  = string.Empty;
    public string Uid { get; set; }  = string.Empty;
}