using AutoMapper;
using SlothOrganizer.Contracts.DTO.Tasks.Task;
using SlothOrganizer.Domain.Entities;

namespace SlothOrganizer.Services.Tasks.Mapping
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<TaskCompletion, TaskCompletionDto>();
            CreateMap<TaskCompletionDto, TaskCompletion>();
            CreateMap<UserTask, TaskDto>();
            CreateMap<TaskDto, UserTask>();
            CreateMap<NewTaskDto, UserTask>();
        }
    }
}
