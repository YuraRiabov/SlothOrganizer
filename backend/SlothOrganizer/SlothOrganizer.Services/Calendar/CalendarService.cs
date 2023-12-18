using AutoMapper;
using Microsoft.Extensions.Configuration;
using SlothOrganizer.Contracts.DTO.Calendar;
using SlothOrganizer.Contracts.DTO.Tasks.Task;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Calendar;
using SlothOrganizer.Services.Abstractions.Utility;

namespace SlothOrganizer.Services.Calendar;

public class CalendarService : ICalendarService
{
    private readonly IHttpService _httpService;
    private readonly ICalendarRepository _calendarRepository;
    private readonly IGoogleOAuthService _googleOAuthService;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public CalendarService(IHttpService httpService, ICalendarRepository calendarRepository,
        IConfiguration configuration, IMapper mapper, IGoogleOAuthService googleOAuthService)
    {
        _httpService = httpService;
        _calendarRepository = calendarRepository;
        _configuration = configuration;
        _mapper = mapper;
        _googleOAuthService = googleOAuthService;
    }

    public async Task CreateGoogleCalendarConnection(TokenResultDto tokenResultDto, long currentUserId)
    {
        var response = await _httpService.SendGetRequest<GoogleCalendarDto>(
            $"{_configuration["GoogleCalendar:GetCalendarAPI"]}/primary", null,
            tokenResultDto.AccessToken);

        var connectedEmail = response.Id;

        if (await _calendarRepository.Get(currentUserId) != null)
        {
            throw new ArgumentException($"Calendar {connectedEmail} is already connected!");
        }

        var calendar = new Domain.Entities.Calendar
        {
            UserId = currentUserId,
            ConnectedCalendar = connectedEmail,
            RefreshToken = tokenResultDto.RefreshToken,
            Uid = Environment.GetEnvironmentVariable("codeVerifier")!
        };

        await _calendarRepository.Insert(calendar);
    }

    public async Task<CalendarDto?> Get(long userId)
    {
        var calendar = await GetInternal(userId);

        return _mapper.Map<CalendarDto?>(calendar);
    }

    public async Task Delete(long calendarId)
    {
        await _calendarRepository.Delete(calendarId);
    }

    public async Task AddEvent(CalendarEventDto dto, long userId)
    {
        var calendar = await GetInternal(userId);
        if (calendar == null)
        {
            throw new EntityNotFoundException(nameof(Domain.Entities.Calendar));
        }

        var token = await _googleOAuthService.RefreshToken(calendar.RefreshToken);
        await AddEvent(dto, token);
    }

    private async Task<Domain.Entities.Calendar?> GetInternal(long userId)
    {
        return await _calendarRepository.Get(userId);
    }
    
    private async Task AddEvent(CalendarEventDto calendarEvent, TokenResultDto token)
    {
        var queryParams = new Dictionary<string, string>
        {
            { "calendarId", "primary" }
        };

        var startTime = calendarEvent.Start;
        var endTime = calendarEvent.End;

        var body = new
        {
            summary = calendarEvent.Name,
            status = "confirmed",
            end = new
            {
                dateTime = endTime
            },
            start = new
            {
                dateTime = startTime
            }
        };

        await _httpService.SendPostTokenRequest<object>($"https://www.googleapis.com/calendar/v3/calendars/calendarId/events", queryParams, body,
            token.AccessToken);
    }
}