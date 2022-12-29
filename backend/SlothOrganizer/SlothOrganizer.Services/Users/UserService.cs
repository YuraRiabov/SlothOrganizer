using AutoMapper;
using SlothOrganizer.Contracts.DTO.Auth;
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
        private readonly ICryptoService _cryptoService;

        public UserService(IRandomService randomService, IMapper mapper, IUserRepository userRepository, IHashService hashService, ICryptoService cryptoService)
        {
            _randomService = randomService;
            _mapper = mapper;
            _userRepository = userRepository;
            _hashService = hashService;
            _cryptoService = cryptoService;
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
            var user = await Find(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> Get(string email)
        {
            var user = await Find(email);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto?> VerifyEmail(string email, int code)
        {
            var user = await _userRepository.VerifyEmail(email, code);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            if(!int.TryParse(_cryptoService.Decrypt(resetPasswordDto.Code), out var code))
            {
                throw new InvalidCredentialsException("Invalid verification code");
            }
            var email = _cryptoService.Decrypt(resetPasswordDto.Email);
            var user = await _userRepository.Get(email, code);
            if (user is null)
            {
                throw new EntityNotFoundException("No user found with such email and code");
            }
            var hash = _hashService.HashPassword(resetPasswordDto.Password, Convert.FromBase64String(user.Salt));
            user.Password = hash;
            user.EmailVerified = true;
            await _userRepository.Update(user);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> Get(LoginDto login)
        {
            var user = await Find(login.Email);
            if (_hashService.VerifyPassword(login.Password, Convert.FromBase64String(user.Salt), user.Password))
            {
                return _mapper.Map<UserDto>(user);
            }
            throw new InvalidCredentialsException("Invalid password");
        }

        private async Task<User> Find(long userId)
        {
            var user = await _userRepository.Get(userId);
            if (user is null)
            {
                throw new EntityNotFoundException("Not found user with such id");
            }
            return user;
        }

        private async Task<User> Find(string email)
        {
            var user = await _userRepository.Get(email);
            if (user is null)
            {
                throw new EntityNotFoundException("No user with such email");
            }
            return user;
        }
    }
}
