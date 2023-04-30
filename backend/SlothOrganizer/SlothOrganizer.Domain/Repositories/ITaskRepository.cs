using SlothOrganizer.Domain.Entities;

namespace SlothOrganizer.Domain.Repositories
{
    public interface ITaskRepository
    {
        Task<List<UserTask>> Get(long dashboardId);
        Task<UserTask> Insert(UserTask task);
        Task<UserTask?> Update(UserTask task);
    }
}
