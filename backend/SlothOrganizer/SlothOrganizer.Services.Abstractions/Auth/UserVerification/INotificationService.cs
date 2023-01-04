namespace SlothOrganizer.Services.Abstractions.Auth.UserVerification
{
    public interface INotificationService
    {
        Task SendPasswordResetLink(string userEmail);
        Task SendVerificationCode(string userEmail);
    }
}
