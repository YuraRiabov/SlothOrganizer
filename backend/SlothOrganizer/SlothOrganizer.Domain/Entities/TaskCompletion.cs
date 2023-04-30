namespace SlothOrganizer.Domain.Entities
{
    public class TaskCompletion
    {
        public long Id { get; set; }
        public long TaskId { get; set; }
        public bool IsSuccessful { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public DateTimeOffset? LastEdited { get; set; }
    }
}
