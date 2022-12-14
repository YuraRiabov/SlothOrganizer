namespace SlothOrganizer.Services.Abstractions.Auth.UserVerification
{
    public interface IUserVerificationService
    {
        Task SendPasswordReset(string userEmail);
        Task SendVerificationCode(string userEmail);
    }
}
