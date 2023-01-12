using SlothOrganizer.Domain.Entities;

namespace SlothOrganizer.Domain.Repositories
{
    public interface ITaskCompletionRepository
    {
        System.Threading.Tasks.Task Delete(long taskId, DateTime repeatingEnd);
        System.Threading.Tasks.Task Delete(long id);
        Task<List<TaskCompletion>> Insert(List<TaskCompletion> taskCompletions);
        Task<TaskCompletion> Update(TaskCompletion taskCompletion);
    }
}
