using Dapper;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Persistence.Properties;

namespace SlothOrganizer.Persistence.Repositories;

public class CalendarRepository : ICalendarRepository
{
    private readonly DapperContext _context;

    public CalendarRepository(DapperContext context)
    {
        _context = context;
    }
    
    public async Task<Calendar> Insert(Calendar calendar)
    {
        var query = Resources.InsertCalendar;

        var parameters = new
        {
            UserId = calendar.UserId,
            ConnectedCalendar = calendar.ConnectedCalendar,
            RefreshToken = calendar.RefreshToken,
            Uid = calendar.Uid
        };
        
        using var connection = _context.CreateConnection();
        calendar.Id = await connection.QuerySingleAsync<long>(query, parameters);
        return calendar;
    }

    public async Task<Calendar?> Get(long userId)
    {
        var query = Resources.GetCalendar;

        var parameters = new
        {
            UserId = userId
        };
        
        using var connection = _context.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Calendar>(query, parameters);
    }
}