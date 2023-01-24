using SlothOrganizer.Contracts.DTO.Tasks.Task.Enums;

namespace SlothOrganizer.Contracts.DTO.Tasks.Task
{
    public class NewTaskDto
    {
        public long DashboardId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; } 
        public DateTime? EndRepeating { get; set; }
        public TaskRepeatingPeriod RepeatingPeriod { get; set; }
    }
}
