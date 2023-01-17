using AutoMapper;
using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Auth.Tokens;
using SlothOrganizer.Services.Abstractions.Users;
using SlothOrganizer.Services.Abstractions.Utility;
using Task = System.Threading.Tasks.Task;

namespace SlothOrganizer.Services.Users
{
    public class UserCredentialsService : IUserCredentialsService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IRandomService _randomService;
        private readonly IHashService _hashService;
        private readonly ICryptoService _cryptoService;
        private readonly ITokenService _tokenService;

        public UserCredentialsService(IRandomService randomService,
            IMapper mapper,
            IUserRepository userRepository,
            IHashService hashService,
            ICryptoService cryptoService,
            ITokenService tokenService)
        {
            _randomService = randomService;
            _mapper = mapper;
            _userRepository = userRepository;
            _hashService = hashService;
            _cryptoService = cryptoService;
            _tokenService = tokenService;
        }

        public async Task<UserAuthDto> SignIn(LoginDto login)
        {
            var user = await Find(login.Email);
            if (!_hashService.VerifyPassword(login.Password, Convert.FromBase64String(user.Salt), user.Password))
            {
                throw new InvalidCredentialsException("Invalid password");
            }
            var token = user.EmailVerified ? await _tokenService.Generate(user.Email) : null;
            return new UserAuthDto
            {
                User = _mapper.Map<UserDto>(user),
                Token = token
            };
        }

        public async Task<UserAuthDto> VerifyEmail(VerificationCodeDto verificationCode)
        {
            var user = await _userRepository.VerifyEmail(verificationCode.Email, verificationCode.VerificationCode);
            if (user is not null)
            {
                return new UserAuthDto
                {
                    User = _mapper.Map<UserDto>(user),
                    Token = await _tokenService.Generate(user.Email)
                };
            }
            throw new InvalidCredentialsException("Invalid verification code");
        }

        public async Task<UserAuthDto> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            if (!int.TryParse(_cryptoService.Decrypt(resetPasswordDto.Code), out var code))
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

            return new UserAuthDto
            {
                User = _mapper.Map<UserDto>(user),
                Token = await _tokenService.Generate(user.Email)
            };
        }

        public async Task UpdatePassword(PasswordUpdateDto passwordUpdate)
        {
            var user = await Find(passwordUpdate.Email);
            if (!_hashService.VerifyPassword(passwordUpdate.OldPassword, Convert.FromBase64String(user.Salt), user.Password))
            {
                throw new InvalidCredentialsException("Invalid password");
            }
            var hash = _hashService.HashPassword(passwordUpdate.Password, Convert.FromBase64String(user.Salt));
            user.Password = hash;
            user.EmailVerified = true;
            await _userRepository.Update(user);
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
