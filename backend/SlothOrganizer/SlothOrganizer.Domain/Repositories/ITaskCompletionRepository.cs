using SlothOrganizer.Domain.Entities;

namespace SlothOrganizer.Domain.Repositories
{
    public interface ITaskCompletionRepository
    {
        Task Delete(long taskId, DateTime endLimit);
        Task Delete(long id);
        Task<IEnumerable<TaskCompletion>> Insert(IEnumerable<TaskCompletion> taskCompletions);
        Task<TaskCompletion?> Update(TaskCompletion taskCompletion);
    }
}
