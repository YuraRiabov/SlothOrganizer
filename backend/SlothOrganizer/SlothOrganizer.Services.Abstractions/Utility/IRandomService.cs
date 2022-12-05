namespace SlothOrganizer.Services.Abstractions.Utility
{
    public interface IRandomService
    {
        byte[] GetRandomBytes(int length = 16);
        int GetRandomNumber(int digitCount = 6);
    }
}
