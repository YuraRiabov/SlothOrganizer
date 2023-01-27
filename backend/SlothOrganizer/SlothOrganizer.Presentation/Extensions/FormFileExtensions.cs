using Microsoft.AspNetCore.Http;

namespace SlothOrganizer.Presentation.Extensions
{
    public static class FormFileExtensions
    {
        public static byte[] GetBytes(this IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
