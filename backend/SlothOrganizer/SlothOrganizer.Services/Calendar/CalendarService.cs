using Microsoft.Extensions.Configuration;
using SlothOrganizer.Contracts.DTO.Calendar;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Calendar;
using SlothOrganizer.Services.Abstractions.Utility;

namespace SlothOrganizer.Services.Calendar;

public class CalendarService : ICalendarService
{
    private readonly IHttpService _httpService;
    private readonly ICalendarRepository _calendarRepository;
    private readonly IConfiguration _configuration;

    public CalendarService(IHttpService httpService, ICalendarRepository calendarRepository,
        IConfiguration configuration)
    {
        _httpService = httpService;
        _calendarRepository = calendarRepository;
        _configuration = configuration;
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
}