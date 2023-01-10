﻿using Dapper;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Persistence.Properties;
using Task = SlothOrganizer.Domain.Entities.Task;

namespace SlothOrganizer.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DapperContext _context;

        public TaskRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Task> Insert(Task task)
        {
            var query = Resources.InsertTask;

            var parameters = new
            {
                DashboardId = task.DashboardId,
                Title = task.Title,
                Description = task.Description,
            };

            using var connection = _context.CreateConnection();
            var id = await connection.QuerySingleAsync<long>(query, parameters);
            task.Id = id;
            return task;
        }

        public async Task<List<Task>> Get(long dashboardId)
        {
            var query = Resources.GetAllTasks;
            var tasks = new Dictionary<long, Task>();

            using var connection = _context.CreateConnection();
            await connection.QueryAsync<Task, TaskCompletion, Task>(query, (task, completion) =>
                {
                    if (!tasks.TryGetValue(task.Id, out var uniqueTask))
                    {
                        tasks.Add(task.Id, task);
                        uniqueTask = task;
                    }
                    uniqueTask.TaskCompletions.Add(completion);
                    return uniqueTask;
                },
                param: new { dashboardId }
            );
            return tasks.Values.ToList();
        }
    }
}