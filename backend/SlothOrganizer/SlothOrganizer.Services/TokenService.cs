using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Services.Abstractions;

namespace SlothOrganizer.Services
{
    public class TokenService : ITokenService
    {
        private readonly ISecurityService _securityService;
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration, ISecurityService securityService)
        {
            _configuration = configuration;
            _securityService = securityService;
        }

        public TokenDto GenerateToken(string email)
        {
            return new TokenDto
            {
                AccessToken = GenerateAccessToken(email),
                RefreshToken = GenerateRefreshToken()
            };
        }

        public string GetEmailFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"])),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new InvalidTokenException("Invalid token");
            }
            return principal.FindFirst(ClaimTypes.Email)?.Value ?? throw new InvalidTokenException("Token doesn't contain email claim");
        }

        public string GenerateAccessToken(string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email)
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(_securityService.GetRandomBytes());
        }
    }
}
