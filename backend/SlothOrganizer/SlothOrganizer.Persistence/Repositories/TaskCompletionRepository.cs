using Dapper;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Persistence.Properties;

namespace SlothOrganizer.Persistence.Repositories
{
    public class TaskCompletionRepository : ITaskCompletionRepository
    {
        private readonly DapperContext _context;

        public TaskCompletionRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<TaskCompletion>> Insert(List<TaskCompletion> taskCompletions)
        {
            var query = Resources.InsertTaskCompletion;

            using var connection = _context.CreateConnection();
            foreach (var taskCompletion in taskCompletions)
            {
                var parameters = new
                {
                    TaskId = taskCompletion.TaskId,
                    IsSuccessful = taskCompletion.IsSuccessful,
                    Start = taskCompletion.Start,
                    End = taskCompletion.End,
                    LastEdited = taskCompletion.LastEdited
                };
                var id = await connection.QuerySingleAsync<long>(query, parameters);
                taskCompletion.Id = id;
            }
            
            return taskCompletions;
        }

        public async Task<TaskCompletion?> Update(TaskCompletion taskCompletion)
        {
            var query = Resources.UpdateTaskCompletion;
            var parameters = new
            {
                Id = taskCompletion.Id,
                IsSuccessful = taskCompletion.IsSuccessful,
                Start = taskCompletion.Start,
                End = taskCompletion.End,
                LastEdited = taskCompletion.LastEdited
            };
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<TaskCompletion>(query, parameters);
        }

        public async Task Delete(long taskId, DateTime repeatingEnd)
        {
            var command = Resources.DeleteTaskCompletions;

            var parameters = new
            {
                TaskId = taskId,
                RepeatingEnd = repeatingEnd
            };

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(command, parameters);
        }

        public async Task Delete(long id)
        {
            var command = Resources.DeleteTaskCompletion;

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(command, new { id });
        }
    }
}
