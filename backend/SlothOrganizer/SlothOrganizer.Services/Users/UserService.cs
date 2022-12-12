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
        private readonly IRandomService _randomService;
        private readonly IHashService _hashService;

        public UserService(IRandomService randomService, IMapper mapper, IUserRepository userRepository, IHashService hashService)
        {
            _randomService = randomService;
            _mapper = mapper;
            _userRepository = userRepository;
            _hashService = hashService;
        }

        public async Task<UserDto> Create(NewUserDto newUser)
        {
            if (await _userRepository.Get(newUser.Email) is not null)
            {
                throw new DuplicateAccountException();
            }
            var salt = _randomService.GetRandomBytes();
            var hashedPassword = _hashService.HashPassword(newUser.Password, salt);
            var user = _mapper.Map<User>(newUser);
            user.Salt = Convert.ToBase64String(salt);
            user.Password = hashedPassword;

            var createdUser = await _userRepository.Insert(user);
            user.Id = createdUser.Id;
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> Get(long id)
        {
            var user = await GetByIdInternal(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<string?> VerifyEmail(long userId, int code)
        {
            return await _userRepository.VerifyEmail(userId, code);
        }

        private async Task<User> GetByIdInternal(long userId)
        {
            var user = await _userRepository.Get(userId);
            if (user is null)
            {
                throw new EntityNotFoundException("Not found user with such id");
            }
            return user;
        }
    }
}
