namespace SlothOrganizer.Services.Abstractions.Auth.Tokens
{
    public interface IAccessTokenService
    {
        string Generate(string email);
        string GetEmailFromToken(string token);
    }
}
