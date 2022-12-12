namespace SlothOrganizer.Contracts.DTO.Auth
{
    public class VerificationCodeDto
    {
        public long UserId { get; set; }
        public int VerificationCode { get; set; }
    }
}
