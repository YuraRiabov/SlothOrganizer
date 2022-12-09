using AutoMapper;
using FakeItEasy;
using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Utility;
using SlothOrganizer.Services.Users;
using Xunit;

namespace SlothOrganizer.Services.Tests.Unit.Users
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly IHashService _hashService;
        private readonly IRandomService _randomService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserServiceTests()
        {
            _hashService = A.Fake<IHashService>();
            _randomService = A.Fake<IRandomService>();
            _userRepository = A.Fake<IUserRepository>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<UserMappingProfile>());
            _mapper = config.CreateMapper();

            _userService = new UserService(_randomService, _mapper, _userRepository, _hashService);
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

            var result = await _userService.Create(newUser);

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

            var code = async () => await _userService.Create(newUser);

            var exception = await Assert.ThrowsAsync<DuplicateAccountException>(code);
            Assert.Equal("Account with this email already exists", exception.Message);
        }

        [Fact]
        public async Task GetUser_WhenExists_ShouldReturn()
        {
            var user = GetUser();
            A.CallTo(() => _userRepository.Get(1)).Returns(Task.FromResult<User?>(user));

            var result = await _userService.Get(1);

            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task GetUser_WhenAbsent_ShouldThrow()
        {
            A.CallTo(() => _userRepository.Get(1)).Returns(Task.FromResult<User?>(null));

            var code = async () => await _userService.Get(1);

            var exception = await Assert.ThrowsAsync<EntityNotFoundException>(code);
            Assert.Equal("Not found user with such id", exception.Message);
        }

        [Fact]
        public async Task VerifyEmail_WhenExists_ShouldUpdate()
        {
            var code = 111111;
            var user = GetUser();
            A.CallTo(() => _userRepository.VerifyEmail(user.Email, code)).Returns(user);

            var result = await _userService.VerifyEmail(user.Email, code);

            Assert.NotNull(result);
            Assert.Equal(user.Email, result.Email);
            A.CallTo(() => _userRepository.VerifyEmail(user.Email, code)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task VerifyEmail_WhenAbsent_ShouldReturnNull()
        {
            A.CallTo(() => _userRepository.VerifyEmail(A<string>._, 111111)).Returns(Task.FromResult<User?>(null));

            var result = await _userService.VerifyEmail("email", 111111);

            Assert.Null(result);
        }

        [Fact]
        public async Task Authorize_WhenValid_ShouldReturn()
        {
            //Arrange
            var bytes = GetBytes();
            var login = new LoginDto
            {
                Email = "test@test.com",
                Password = "test"
            };
            var user = new User
            {
                Email = "test@test.com",
                Salt = Convert.ToBase64String(bytes),
                Password = "hashedTest"
            };
            A.CallTo(() => _userRepository.Get("test@test.com")).Returns(Task.FromResult<User?>(user));
            A.CallTo(() => _hashService.VerifyPassword(login.Password, A<byte[]>._, user.Password)).Returns(true);

            //Act
            var result = await _userService.Authorize(login);

            //Assert
            Assert.Equal(login.Email, result.Email);
            A.CallTo(() => _hashService.VerifyPassword(login.Password, A<byte[]>._, user.Password)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Authorize_WhenInValid_ShouldThrow()
        {
            //Arrange
            var bytes = new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            var auth = new LoginDto
            {
                Email = "test@test.com",
                Password = "test"
            };
            var user = new User
            {
                Email = "test@test.com",
                Salt = Convert.ToBase64String(bytes),
                Password = "hashedTest"
            };
            A.CallTo(() => _userRepository.Get("test@test.com")).Returns(Task.FromResult<User?>(user));
            A.CallTo(() => _hashService.VerifyPassword(auth.Password, A<byte[]>._, user.Password)).Returns(false);

            //Act
            var code = async () => await _userService.Authorize(auth);

            //Assert
            await Assert.ThrowsAsync<InvalidCredentialsException>(code);
            A.CallTo(() => _hashService.VerifyPassword(auth.Password, A<byte[]>._, user.Password)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetByEmail_WhenExists_ShouldReturn()
        {
            // Arrange
            var user = GetUser();
            A.CallTo(() => _userRepository.Get(user.Email)).Returns(Task.FromResult<User?>(user));

            //Act
            var result = await _userService.Get("test@test.com");

            //Assert
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task GetByEmail_WhenAbsent_ShouldThrow()
        {
            // Arrange
            A.CallTo(() => _userRepository.Get("test")).Returns(Task.FromResult<User?>(null));

            //Act
            var code = async () => await _userService.Get("test");

            //Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(code);
        }

        private byte[] GetBytes()
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
            };
        }
    }
}
