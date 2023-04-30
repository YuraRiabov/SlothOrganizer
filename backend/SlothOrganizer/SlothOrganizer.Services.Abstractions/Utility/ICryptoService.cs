namespace SlothOrganizer.Services.Abstractions.Utility
{
    public interface ICryptoService
    {
        string Decrypt(string value);
        string Encrypt(string value);
    }
}
