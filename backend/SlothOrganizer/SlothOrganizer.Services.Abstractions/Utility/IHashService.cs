namespace SlothOrganizer.Services.Abstractions.Utility
{
    public interface IHashService
    {
        string HashPassword(string password, byte[] salt);
    }
}
