namespace SlothOrganizer.Services.Abstractions.Utility
{
    public interface IHashService
    {
        string HashPassword(string password, byte[] salt);
        bool VerifyPassword(string password, byte[] salt, string hash);
    }
}
