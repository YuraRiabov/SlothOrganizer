using AutoMapper;
using SlothOrganizer.Contracts.DTO.Calendar;
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
            CreateMap<ExportTaskCompletionDto, CalendarEventDto>()
                .ForMember(ce => ce.Name, opt => opt.MapFrom((tc) => tc.TaskName));
        }
    }
}