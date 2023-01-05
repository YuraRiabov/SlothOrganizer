namespace SlothOrganizer.Contracts.DTO.Auth
{
    public class VerificationCodeDto
    {
        public string Email { get; set; }
        public int VerificationCode { get; set; }
    }
}
