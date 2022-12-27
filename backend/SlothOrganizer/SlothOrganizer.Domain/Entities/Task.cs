namespace SlothOrganizer.Domain.Entities
{
    public class Task
    {
        public long Id { get; set; }
        public long DashboardId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public List<TaskCompletion> TaskCompletions { get; set; } = new List<TaskCompletion>();
    }
}
