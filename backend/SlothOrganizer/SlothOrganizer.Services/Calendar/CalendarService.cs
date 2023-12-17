using AutoMapper;
using Microsoft.Extensions.Configuration;
using SlothOrganizer.Contracts.DTO.Calendar;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Calendar;
using SlothOrganizer.Services.Abstractions.Utility;

namespace SlothOrganizer.Services.Calendar;

public class CalendarService : ICalendarService
{
    private readonly IHttpService _httpService;
    private readonly ICalendarRepository _calendarRepository;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public CalendarService(IHttpService httpService, ICalendarRepository calendarRepository,
        IConfiguration configuration, IMapper mapper)
    {
        _httpService = httpService;
        _calendarRepository = calendarRepository;
        _configuration = configuration;
        _mapper = mapper;
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
        var calendar = await _calendarRepository.Get(userId);

        return _mapper.Map<CalendarDto?>(calendar);
    }

    public async Task Delete(long calendarId)
    {
        await _calendarRepository.Delete(calendarId);
    }
}