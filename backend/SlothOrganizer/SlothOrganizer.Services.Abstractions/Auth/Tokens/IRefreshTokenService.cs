namespace SlothOrganizer.Services.Abstractions.Auth.Tokens
{
    public interface IRefreshTokenService
    {
        public Task<string> Generate(string userEmail);
        public Task<bool> Validate(string userEmail, string token);
    }
}
