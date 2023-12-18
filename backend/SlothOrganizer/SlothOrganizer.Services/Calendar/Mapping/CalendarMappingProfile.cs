using AutoMapper;
using SlothOrganizer.Contracts.DTO.Calendar;

namespace SlothOrganizer.Services.Calendar.Mapping;

public class CalendarMappingProfile : Profile
{
    public CalendarMappingProfile()
    {
        CreateMap<Domain.Entities.Calendar, CalendarDto>();
    }
}