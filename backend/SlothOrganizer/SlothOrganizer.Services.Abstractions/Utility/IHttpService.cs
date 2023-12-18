namespace SlothOrganizer.Services.Abstractions.Utility;

public interface IHttpService
{
    Task<T> SendGetRequest<T>(string endpoint, Dictionary<string, string>? queryParams, string accessToken);
    Task<T> SendPostRequest<T>(string endpoint, Dictionary<string, string>? bodyParams);
    Task<T> SendPostTokenRequest<T>(string endpoint, Dictionary<string, string>? queryParams, object body, string accessToken);
    Task SendPutRequest(string endpoint, Dictionary<string, string>? queryParams, object body, string accessToken);
}