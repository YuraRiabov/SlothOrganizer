using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SlothOrganizer.Presentation.Extensions;
using SlothOrganizer.Services.Abstractions.Calendar;

namespace SlothOrganizer.Presentation.Controllers;

[ApiController]
[Route("googleOAuth")]
public class GoogleOAuthController : ControllerBase
{
    private readonly IGoogleOAuthService _authService;
    private readonly ICalendarService _calendarsService;
    private readonly IConfiguration _configuration;
    private static long CurrentUserId { get; set; }
    
    public GoogleOAuthController(IGoogleOAuthService authService, ICalendarService calendarService, IConfiguration configuration)
    {
        _calendarsService = calendarService;
        _authService = authService;
        _configuration = configuration;
    }
    
    [Authorize]
    [HttpGet("redirect")]
    public ActionResult<string> RedirectOnAuthServer()
    {
        CurrentUserId = HttpContext.User.GetId();
        return Ok(_authService.GetAuthorizationUrl());
    }
    
    [HttpGet("code")]
    public async Task<IActionResult> Code(string code)
    {
        var codeVerifier = Environment.GetEnvironmentVariable("codeVerifier");
        var tokenResult = await _authService.ExchangeCodeOnToken(code, codeVerifier!, _configuration["GoogleCalendar:RedirectUrl"]);
        await _calendarsService.CreateGoogleCalendarConnection(tokenResult, CurrentUserId);
        return Redirect(_configuration["GoogleCalendar:CalendarsPageUrl"]);
    }
}