namespace SlothOrganizer.Contracts.DTO.Tasks.Task
{
    public class TaskDto
    {
        public long Id { get; set; }
        public long DashboardId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<TaskCompletionDto> TaskCompletions { get; set; } = new List<TaskCompletionDto>();
    }
}
