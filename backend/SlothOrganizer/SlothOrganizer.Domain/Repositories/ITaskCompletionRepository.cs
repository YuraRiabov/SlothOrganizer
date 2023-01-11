using SlothOrganizer.Domain.Entities;

namespace SlothOrganizer.Domain.Repositories
{
    public interface ITaskCompletionRepository
    {
        System.Threading.Tasks.Task Delete(long taskId, DateTime repeatingEnd);
        Task<List<TaskCompletion>> Insert(List<TaskCompletion> taskCompletions);
        Task<TaskCompletion> Update(TaskCompletion taskCompletion);
    }
}
