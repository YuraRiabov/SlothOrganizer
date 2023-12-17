using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlothOrganizer.Presentation.Extensions;
using SlothOrganizer.Services.Abstractions.Calendar;

namespace SlothOrganizer.Presentation.Controllers;

[ApiController]
[Route("calendar")]
public class CalendarController : ControllerBase
{
    private readonly ICalendarService _calendarsService;
    
    public CalendarController(ICalendarService calendarService)
    {
        _calendarsService = calendarService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetCalendar()
    {
        var userId = HttpContext.User.GetId();
        return Ok(await _calendarsService.Get(userId));
    }

    [Authorize]
    [HttpDelete("{calendarId}")]
    public async Task<IActionResult> DeleteCalendar(long calendarId)
    {
        await _calendarsService.Delete(calendarId);
        return NoContent();
    }
}