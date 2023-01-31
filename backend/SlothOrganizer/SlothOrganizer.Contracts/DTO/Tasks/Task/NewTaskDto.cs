using SlothOrganizer.Contracts.DTO.Tasks.Task.Enums;

namespace SlothOrganizer.Contracts.DTO.Tasks.Task
{
    public class NewTaskDto
    {
        public long DashboardId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; } 
        public DateTimeOffset? EndRepeating { get; set; }
        public TaskRepeatingPeriod RepeatingPeriod { get; set; }
    }
}
