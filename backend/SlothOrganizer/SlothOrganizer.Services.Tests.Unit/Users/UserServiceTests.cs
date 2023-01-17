using AutoMapper;
using FakeItEasy;
using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Auth.Tokens;
using SlothOrganizer.Services.Abstractions.Auth.UserVerification;
using SlothOrganizer.Services.Abstractions.Utility;
using SlothOrganizer.Services.Auth.Tokens;
using SlothOrganizer.Services.Users;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace SlothOrganizer.Services.Tests.Unit.Users
{
    public class UserServiceTests
    {
        private readonly UserCredentialsService _userCredentialsService;
        private readonly IHashService _hashService;
        private readonly IRandomService _randomService;
        private readonly IUserRepository _userRepository;
        private readonly ICryptoService _cryptoService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserServiceTests()
        {
            _hashService = A.Fake<IHashService>();
            _randomService = A.Fake<IRandomService>();
            _userRepository = A.Fake<IUserRepository>();
            _cryptoService = A.Fake<ICryptoService>();
            _tokenService = A.Fake<ITokenService>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<UserMappingProfile>());
            _mapper = config.CreateMapper();

            _userCredentialsService = new UserCredentialsService(_randomService,
                _mapper,
                _userRepository,
                _hashService,
                _cryptoService,
                _tokenService);
        }


        [Fact]
        public async Task VerifyEmail_WhenValid_ShouldVerify()
        {
            var verificationCodeDto = GetVerificationCodeDto();
            var user = GetUser(verificationCodeDto.Email, true);

            A.CallTo(() => _userRepository.VerifyEmail(user.Email, verificationCodeDto.VerificationCode)).Returns(user);
            A.CallTo(() => _tokenService.Generate(verificationCodeDto.Email)).Returns(new TokenDto { AccessToken = "test" });

            var result = await _userCredentialsService.VerifyEmail(verificationCodeDto);

            Assert.Equal("test", result.Token.AccessToken);
        }

        [Fact]
        public async Task VerifyEmail_WhenInvalid_ShouldThrow()
        {
            var verificationCodeDto = GetVerificationCodeDto();
            A.CallTo(() => _userRepository.VerifyEmail(A<string>._, 111111)).Returns(Task.FromResult<User?>(null));

            var code = async () => await _userCredentialsService.VerifyEmail(verificationCodeDto);

            var exception = await Assert.ThrowsAsync<InvalidCredentialsException>(code);
            Assert.Equal("Invalid verification code", exception.Message);
        }

        [Fact]
        public async Task CreateUser_WhenValidData_ShouldAdd()
        {
            A.CallTo(() => _userRepository.Get(A<string>._)).Returns(Task.FromResult<User?>(null));
            A.CallTo(() => _userRepository.Insert(A<User>._)).Returns(new User { Id = 1 });

            var salt = GetBytes();
            A.CallTo(() => _randomService.GetRandomBytes(16)).Returns(salt);
            A.CallTo(() => _hashService.HashPassword(A<string>._, salt)).Returns("hash");
            NewUserDto newUser = GetNewUser();

            var result = await _userCredentialsService.Create(newUser);

            Assert.Equal(1, result.Id);
            A.CallTo(() => _randomService.GetRandomBytes(16)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _hashService.HashPassword(A<string>._, salt)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _userRepository.Insert(A<User>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task CreateUser_WhenEmailExists_ShouldThrow()
        {
            A.CallTo(() => _userRepository.Get(A<string>._)).Returns(Task.FromResult<User?>(new User()));
            A.CallTo(() => _userRepository.Insert(A<User>._)).Returns(new User { Id = 1 });

            var salt = GetBytes();
            A.CallTo(() => _randomService.GetRandomBytes(16)).Returns(salt);
            A.CallTo(() => _hashService.HashPassword(A<string>._, salt)).Returns("hash");

            var newUser = GetNewUser();

            var code = async () => await _userCredentialsService.Create(newUser);

            var exception = await Assert.ThrowsAsync<DuplicateAccountException>(code);
            Assert.Equal("Account with this email already exists", exception.Message);
        }

        [Fact]
        public async Task ResetPassword_WhenValidEmail_ShouldRefresh()
        {
            var dto = GetResetPasswordDto();
            var user = GetUser();

            A.CallTo(() => _cryptoService.Decrypt(dto.Code)).Returns(dto.Code);
            A.CallTo(() => _cryptoService.Decrypt(dto.Email)).Returns(dto.Email);
            A.CallTo(() => _userRepository.Get(dto.Email, int.Parse(dto.Code))).Returns(user);
            A.CallTo(() => _hashService.HashPassword(dto.Password, A<byte[]>._)).Returns("hashed");

            await _userCredentialsService.ResetPassword(dto);

            A.CallTo(() => _userRepository.Update(user)).MustHaveHappenedOnceExactly();
            Assert.Equal("hashed", user.Password);
        }

        [Fact]
        public async Task ResetPassword_WhenInvalidEmail_ShouldThrow()
        {
            var dto = GetResetPasswordDto();

            A.CallTo(() => _cryptoService.Decrypt(dto.Code)).Returns(dto.Code);
            A.CallTo(() => _cryptoService.Decrypt(dto.Email)).Returns(dto.Email);
            A.CallTo(() => _userRepository.Get(dto.Email, int.Parse(dto.Code))).Returns(Task.FromResult<User?>(null));

            var code = async () => await _userCredentialsService.ResetPassword(dto);

            var exception = await Assert.ThrowsAsync<EntityNotFoundException>(code);
            Assert.Equal("No user found with such email and code", exception.Message);
        }

        [Fact]
        public async Task ResetPassword_WhenInvalidCode_ShouldThrow()
        {
            var dto = GetResetPasswordDto();

            A.CallTo(() => _cryptoService.Decrypt(dto.Code)).Returns("not a number");

            var code = async () => await _userCredentialsService.ResetPassword(dto);

            var exception = await Assert.ThrowsAsync<InvalidCredentialsException>(code);
            Assert.Equal("Invalid verification code", exception.Message);
        }

        [Fact]
        public async Task SignIn_WhenEmailVerified_ShouldReturnWithToken()
        {
            var bytes = GetBytes();
            var login = GetLoginDto();
            var token = GetTokenDto();
            var user = new User
            {
                Email = "test@test.com",
                Salt = Convert.ToBase64String(bytes),
                Password = "hashedTest",
                EmailVerified = true
            };
            A.CallTo(() => _userRepository.Get("test@test.com")).Returns(Task.FromResult<User?>(user));
            A.CallTo(() => _hashService.VerifyPassword(login.Password, A<byte[]>._, user.Password)).Returns(true);
            A.CallTo(() => _tokenService.Generate(user.Email)).Returns(token);

            var result = await _userCredentialsService.SignIn(login);

            Assert.NotNull(result.Token);
            Assert.Equal(login.Email, result.User.Email);
            Assert.Equal(token.AccessToken, result.Token!.AccessToken);
            Assert.Equal(token.RefreshToken, result.Token.RefreshToken);
        }

        [Fact]
        public async Task SignIn_WhenEmailNotVerified_ShouldReturnWithoutToken()
        {
            var bytes = GetBytes();
            var login = GetLoginDto();
            var token = GetTokenDto();
            var user = new User
            {
                Email = "test@test.com",
                Salt = Convert.ToBase64String(bytes),
                Password = "hashedTest"
            };
            A.CallTo(() => _userRepository.Get("test@test.com")).Returns(Task.FromResult<User?>(user));
            A.CallTo(() => _hashService.VerifyPassword(login.Password, A<byte[]>._, user.Password)).Returns(true);
            A.CallTo(() => _tokenService.Generate(user.Email)).Returns(token);

            var result = await _userCredentialsService.SignIn(login);

            Assert.Null(result.Token);
            Assert.Equal(login.Email, result.User.Email);
        }

        private ResetPasswordDto GetResetPasswordDto()
        {
            return new ResetPasswordDto
            {
                Email = "test@test.com",
                Password = "test",
                Code = "111111"
            };
        }

        private static byte[] GetBytes()
        {
            return new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }
        private static NewUserDto GetNewUser()
        {
            return new NewUserDto
            {
                FirstName = "test",
                LastName = "user",
                Email = "test@test.com",
                Password = "password"
            };
        }
        private static User GetUser()
        {
            return new User
            {
                Id = 1,
                FirstName = "test",
                LastName = "user",
                Email = "test@test.com",
                Salt = Convert.ToBase64String(GetBytes())
            };
        }

        private static LoginDto GetLoginDto()
        {
            return new LoginDto
            {
                Email = "test@test.com",
                Password = "test"
            };
        }
        private VerificationCodeDto GetVerificationCodeDto()
        {
            return new VerificationCodeDto
            {
                Email = "test@test.com",
                VerificationCode = 111111
            };
        }

        private static User GetUser(string email, bool emailVerified)
        {
            return new User
            {
                Email = email,
                Id = 1,
                EmailVerified = emailVerified
            };
        }

        private static TokenDto GetTokenDto()
        {
            return new TokenDto
            {
                AccessToken = "expired",
                RefreshToken = "notExpired"
            };
        }
    }
}
