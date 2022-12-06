namespace SlothOrganizer.Services.Abstractions.Auth
{
    public interface IVerificationCodeService
    {
        Task<int> Generate(long userId);

        Task<bool> Verify(long userId, int code);
    }
}
