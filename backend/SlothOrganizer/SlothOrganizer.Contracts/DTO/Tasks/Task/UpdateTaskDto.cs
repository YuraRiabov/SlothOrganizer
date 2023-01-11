namespace SlothOrganizer.Contracts.DTO.Tasks.Task
{
    public class UpdateTaskDto
    {
        public TaskDto Task { get; set; }
        public TaskCompletionDto TaskCompletion { get; set; }
        public DateTime? EndRepeating { get; set; }
    }
}
