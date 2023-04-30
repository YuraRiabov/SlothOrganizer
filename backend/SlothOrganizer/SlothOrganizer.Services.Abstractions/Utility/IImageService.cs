namespace SlothOrganizer.Services.Abstractions.Utility
{
    public interface IImageService
    {
        Task<string> Upload(byte[] image, string fileName);
    }
}
