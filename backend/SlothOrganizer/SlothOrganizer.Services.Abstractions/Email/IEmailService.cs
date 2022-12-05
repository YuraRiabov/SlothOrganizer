namespace SlothOrganizer.Services.Abstractions.Email
{
    public interface IEmailService
    {
        Task SendEmail(string to, string subject, string message);
    }
}
