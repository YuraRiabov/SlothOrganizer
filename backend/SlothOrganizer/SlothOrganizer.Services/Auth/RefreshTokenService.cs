using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Auth;
using SlothOrganizer.Services.Abstractions.Utility;

namespace SlothOrganizer.Services.Auth
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ISecurityService _securityService;
        private readonly IDateTimeService _dateTimeService;

        public RefreshTokenService(IDateTimeService dateTimeService, ISecurityService securityService, IRefreshTokenRepository refreshTokenRepository)
        {
            _dateTimeService = dateTimeService;
            _securityService = securityService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<string> GenerateRefreshToken(long userId)
        {
            var token = new RefreshToken
            {
                UserId = userId,
                Token = GenerateToken(),
                ExpirationTime = _dateTimeService.Now().AddDays(7)
            };
            await _refreshTokenRepository.Insert(token);
            return token.Token;
        }

        public async Task<bool> ValidateRefreshToken(long userId, string token)
        {
            var userTokens = await _refreshTokenRepository.GetByUserId(userId);
            return userTokens.Any(t => t.Token == token && t.ExpirationTime > _dateTimeService.Now());
        }


        private string GenerateToken()
        {
            return Convert.ToBase64String(_securityService.GetRandomBytes());
        }
    }
}
