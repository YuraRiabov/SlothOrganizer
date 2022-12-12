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
    public class TokenService : ITokenService
    {
        private readonly IRandomService _randomService;
        private readonly IDateTimeService _dateTimeService;
        private readonly JwtOptions _jwtOptions;

        public TokenService(IOptions<JwtOptions> options, IRandomService randomService, IDateTimeService dateTimeService)
        {
            _jwtOptions = options.Value;
            _randomService = randomService;
            _dateTimeService = dateTimeService;
        }

        public TokenDto GenerateToken(string email)
        {
            return new TokenDto
            {
                AccessToken = GenerateAccessToken(email),
                RefreshToken = GenerateRefreshToken()
            };
        }

        public string GetEmailFromToken(string token)
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
            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new Exception();
                }
                return principal.FindFirst(ClaimTypes.Email)?.Value ?? throw new InvalidCredentialsException("Token doesn't contain email claim");
            }
            catch (Exception)
            {
                throw new InvalidCredentialsException("Invalid token");
            }
        }

        private string GenerateAccessToken(string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email)
            };
            var token = new JwtSecurityToken(_jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                expires: _dateTimeService.Now().AddMinutes(60),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(_randomService.GetRandomBytes());
        }
    }
}
