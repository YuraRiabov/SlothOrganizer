using System.Globalization;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Services.Abstractions.Utility;

namespace SlothOrganizer.Services.Utility.Gyazo
{
    public class GyazoService : IImageService
    {
        private readonly GyazoOptions _options;
        private readonly HttpClient _httpClient;
        public GyazoService(IOptions<GyazoOptions> options, HttpClient httpClient)
        {
            _options = options.Value;
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _options.AccessToken);
        }

        public async Task<string> Upload(byte[] image, string fileName)
        {
            using var content = new MultipartFormDataContent();
            using var imageStream = new MemoryStream(image);
            content.Add(new StreamContent(imageStream), "imagedata", fileName);
            var response = await _httpClient.PostAsync(_options.UploadUrl, content);
            var contentString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GyazoResponseDto>(contentString).Url;
        }
    }
}
