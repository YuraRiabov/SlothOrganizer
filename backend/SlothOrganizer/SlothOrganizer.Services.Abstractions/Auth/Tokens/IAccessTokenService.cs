namespace SlothOrganizer.Services.Abstractions.Auth.Tokens
{
    public interface IAccessTokenService
    {
        string Generate(string email, long id);
        string GetEmail(string token);
        long GetId(string token);
    }
}
