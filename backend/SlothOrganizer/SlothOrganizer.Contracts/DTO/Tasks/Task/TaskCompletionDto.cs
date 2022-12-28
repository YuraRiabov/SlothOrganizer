namespace SlothOrganizer.Contracts.DTO.Tasks.Task
{
    public class TaskCompletionDto
    {
        public long Id { get; set; }
        public long TaskId { get; set; }
        public bool IsSuccessful { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime? LastEdited { get; set; }
    }
}
