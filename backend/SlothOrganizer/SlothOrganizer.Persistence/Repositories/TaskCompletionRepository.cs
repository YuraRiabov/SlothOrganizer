using System.Threading.Tasks;
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
            connection.Open();
            using var transaction = connection.BeginTransaction();
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
                var id = await connection.QuerySingleAsync<long>(query, parameters, transaction);
                taskCompletion.Id = id;
            }
            transaction.Commit();
            
            return taskCompletions;
        }
    }
}
