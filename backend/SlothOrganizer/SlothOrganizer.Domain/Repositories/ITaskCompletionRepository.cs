using SlothOrganizer.Domain.Entities;

namespace SlothOrganizer.Domain.Repositories
{
    public interface ITaskCompletionRepository
    {
        Task<List<TaskCompletion>> Insert(List<TaskCompletion> taskCompletions);
    }
}
