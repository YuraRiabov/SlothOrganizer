﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Services.Abstractions.Auth;
using SlothOrganizer.Services.Abstractions.Utility;

namespace SlothOrganizer.Services.Auth
{
    public class TokenService : ITokenService
    {
        private readonly ISecurityService _securityService;
        private readonly IConfiguration _configuration;
        private readonly IDateTimeService _dateTimeService;

        public TokenService(IConfiguration configuration, ISecurityService securityService, IDateTimeService dateTimeService)
        {
            _configuration = configuration;
            _securityService = securityService;
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
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"])),
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
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email)
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: _dateTimeService.Now().AddMinutes(60),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(_securityService.GetRandomBytes());
        }
    }
}