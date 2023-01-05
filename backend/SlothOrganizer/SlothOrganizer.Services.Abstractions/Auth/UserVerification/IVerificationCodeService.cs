namespace SlothOrganizer.Services.Abstractions.Auth.UserVerification
{
    public interface IVerificationCodeService
    {
        Task<int> Generate(string userEmail);

        Task<bool> Verify(string userEmail, int code);
    }
}
