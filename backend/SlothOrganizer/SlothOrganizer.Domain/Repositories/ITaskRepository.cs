namespace SlothOrganizer.Domain.Repositories
{
    public interface ITaskRepository
    {
        Task<List<Entities.Task>> Get(long dashboardId);
        Task<Entities.Task> Insert(Entities.Task task);
        Task<Entities.Task?> Update(Entities.Task task);
    }
}
