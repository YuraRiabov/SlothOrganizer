namespace SlothOrganizer.Domain.Entities
{
    public class RefreshToken
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Token { get; set; }
        public DateTimeOffset ExpirationTime { get; set; }

        public User? User { get; set; }
    }
}
