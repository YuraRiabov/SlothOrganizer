namespace SlothOrganizer.Contracts.DTO.User
{
    public class PasswordUpdateDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }
    }
}
