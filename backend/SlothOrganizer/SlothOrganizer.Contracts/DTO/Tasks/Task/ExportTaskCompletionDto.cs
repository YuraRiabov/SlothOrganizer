namespace SlothOrganizer.Contracts.DTO.Tasks.Task;

public class ExportTaskCompletionDto
{
    public long Id { get; set; }
    public string TaskName { get; set; } = string.Empty;
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
}