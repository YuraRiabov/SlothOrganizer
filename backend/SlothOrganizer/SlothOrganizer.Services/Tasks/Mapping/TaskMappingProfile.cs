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
            CreateMap<UserTask, TaskDto>();
            CreateMap<NewTaskDto, UserTask>();
        }
    }
}
