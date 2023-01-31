namespace SlothOrganizer.Domain.Entities
{
    public class VerificationCode
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int Code { get; set; }
        public DateTimeOffset ExpirationTime { get; set; }

        public User? User { get; set; }
    }
}
