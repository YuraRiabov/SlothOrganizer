using SlothOrganizer.Domain.Entities;

namespace SlothOrganizer.Domain.Repositories
{
    public interface ITaskCompletionRepository
    {
        Task Delete(long taskId, DateTimeOffset repeatingEnd);
        Task Delete(long id);
        Task<List<TaskCompletion>> Insert(List<TaskCompletion> taskCompletions);
        Task<TaskCompletion?> Update(TaskCompletion taskCompletion);
    }
}
