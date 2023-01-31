using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Services.Abstractions.Auth.Tokens;
using SlothOrganizer.Services.Abstractions.Utility;
using SlothOrganizer.Services.Auth.Tokens.Options;

namespace SlothOrganizer.Services.Auth.Tokens
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly JwtOptions _jwtOptions;
        public AccessTokenService(IOptions<JwtOptions> options, IDateTimeService dateTimeService)
        {
            _jwtOptions = options.Value;
            _dateTimeService = dateTimeService;
        }

        public string GetEmail(string token)
        {
            return GetFromToken(token, ClaimTypes.Email);
        }

        public long GetId(string token)
        {
            return long.Parse(GetFromToken(token, ClaimTypes.NameIdentifier));
        }

        public string Generate(string email, long id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.NameIdentifier, id.ToString())
            };
            var token = new JwtSecurityToken(_jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                expires: _dateTimeService.Now().DateTime.AddMinutes(60),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GetFromToken(string token, string claim)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            ClaimsPrincipal principal;
            try
            {
                principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            }
            catch (Exception)
            {
                throw new InvalidCredentialsException("Invalid token");
            }
            return principal.FindFirst(claim)?.Value ?? throw new InvalidCredentialsException("Token doesn't contain claim");
        }
    }
}
