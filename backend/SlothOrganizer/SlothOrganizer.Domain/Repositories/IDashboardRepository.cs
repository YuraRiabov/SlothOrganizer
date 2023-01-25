using SlothOrganizer.Domain.Entities;

namespace SlothOrganizer.Domain.Repositories
{
    public interface IDashboardRepository
    {
        Task<List<Dashboard>> Get(string email);
        Task<Dashboard> Insert(string title, string userEmail);
    }
}
