﻿using System.Text.Json.Serialization;
using Dapper;
using Newtonsoft.Json;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Persistence.Properties;

namespace SlothOrganizer.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DapperContext _context;

        public TaskRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<UserTask> Insert(UserTask task)
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

        public async Task<List<UserTask>> Get(long dashboardId)
        {
            var query = Resources.GetAllTasks;
            var tasks = new Dictionary<long, UserTask>();

            using var connection = _context.CreateConnection();
            await connection.QueryAsync<UserTask, TaskCompletion, UserTask>(query, (task, completion) =>
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

        public async Task<UserTask?> Update(UserTask task)
        {
            var query = Resources.UpdateTask;

            var parameters = new
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
            };

            var connection = _context.CreateConnection();
            var updatedTask = await connection.QueryFirstAsync(query, parameters);
            return new UserTask
            {
                Id = updatedTask.Id,
                Title = updatedTask.Title,
                Description = updatedTask.Description,
                DashboardId = updatedTask.DashboardId,
                TaskCompletions = JsonConvert.DeserializeObject<List<TaskCompletion>>(updatedTask.TaskCompletions)
            };
        }
    }
}
