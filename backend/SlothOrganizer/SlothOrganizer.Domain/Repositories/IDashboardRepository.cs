using SlothOrganizer.Domain.Entities;

namespace SlothOrganizer.Domain.Repositories
{
    public interface IDashboardRepository
    {
        Task<List<Dashboard>> Get(long userId);
        Task<Dashboard> Insert(Dashboard dashboard);
    }
}
