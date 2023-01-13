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
            CreateMap<Domain.Entities.Task, TaskDto>();
            CreateMap<TaskDto, Domain.Entities.Task>();
            CreateMap<NewTaskDto, Domain.Entities.Task>();
        }
    }
}
