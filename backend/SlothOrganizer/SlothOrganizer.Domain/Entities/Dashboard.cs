namespace SlothOrganizer.Domain.Entities
{
    public class Dashboard
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Title { get; set; }
        public List<Task> Tasks { get; set; } = new List<Task>();
    }
}
