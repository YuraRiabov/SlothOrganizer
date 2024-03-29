﻿using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Auth.Tokens;
using SlothOrganizer.Services.Abstractions.Utility;

namespace SlothOrganizer.Services.Auth.Tokens
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IRandomService _randomService;
        private readonly IDateTimeService _dateTimeService;

        public RefreshTokenService(IDateTimeService dateTimeService, IRandomService randomService, IRefreshTokenRepository refreshTokenRepository)
        {
            _dateTimeService = dateTimeService;
            _randomService = randomService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<string> Generate(string userEmail)
        {
            var token = new RefreshToken
            {
                Token = GenerateToken(),
                ExpirationTime = _dateTimeService.Now().AddDays(7)
            };
            await _refreshTokenRepository.Insert(token, userEmail);
            return token.Token;
        }

        public async Task<bool> Validate(string userEmail, string token)
        {
            var userTokens = await _refreshTokenRepository.Get(userEmail);
            return userTokens.Any(t => t.Token == token && t.ExpirationTime > _dateTimeService.Now());
        }

        private string GenerateToken()
        {
            return Convert.ToBase64String(_randomService.GetRandomBytes());
        }
    }
}
