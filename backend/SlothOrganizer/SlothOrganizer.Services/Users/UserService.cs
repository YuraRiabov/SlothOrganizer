using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Users;
using SlothOrganizer.Services.Abstractions.Utility;

namespace SlothOrganizer.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ISecurityService _securityService;

        public UserService(ISecurityService securityService, IMapper mapper, IUserRepository userRepository)
        {
            _securityService = securityService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDto> CreateUser(NewUserDto newUser)
        {
            if (await _userRepository.GetByEmail(newUser.Email) is not null)
            {
                throw new DuplicateAccountException("Account with this email already exists");
            }
            var salt = _securityService.GetRandomBytes();
            var hashedPassword = _securityService.HashPassword(newUser.Password, salt);
            var user = _mapper.Map<User>(newUser);
            user.Salt = Convert.ToBase64String(salt);
            user.Password = hashedPassword;

            var createdUser = await _userRepository.Insert(user);
            user.Id = createdUser.Id;
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUser(long id)
        {
            var user = await GetByIdInternal(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task VerifyEmail(long userId)
        {
            var user = await GetByIdInternal(userId);
            user.EmailVerified = true;
            await _userRepository.Update(user);
        }

        private async Task<User> GetByIdInternal(long userId)
        {
            var user = await _userRepository.GetById(userId);
            if (user is null)
            {
                throw new EntityNotFoundException("Not found user with such id");
            }
            return user;
        }
    }
}
