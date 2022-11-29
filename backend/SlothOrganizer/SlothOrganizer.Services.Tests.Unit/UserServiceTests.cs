using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FakeItEasy;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Utility;
using SlothOrganizer.Services.Users;
using Xunit;

namespace SlothOrganizer.Services.Tests.Unit
{
    public class UserServiceTests
    {
        private readonly UserService _sut;
        private readonly ISecurityService _securityService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserServiceTests()
        {
            _securityService = A.Fake<ISecurityService>();
            _userRepository = A.Fake<IUserRepository>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>());
            _mapper = config.CreateMapper();

            _sut = new UserService(_securityService, _mapper, _userRepository);
        }

        [Fact]
        public async Task CreateUser_WhenValidData_ShouldAdd()
        {
            //Arrange
            A.CallTo(() => _userRepository.GetByEmail(A<string>._)).Returns(Task.FromResult<User?>(null));
            A.CallTo(() => _userRepository.Insert(A<User>._)).Returns(new User { Id = 1 });

            var salt = new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            A.CallTo(() => _securityService.GetRandomBytes(16)).Returns(salt);
            A.CallTo(() => _securityService.HashPassword(A<string>._, salt)).Returns("hash");

            var newUser = new NewUserDto
            {
                FirstName = "test",
                LastName = "user",
                Email = "test@test.com",
                Password = "password"
            };

            //Act
            var result = await _sut.CreateUser(newUser);

            //Assert
            Assert.Equal(1, result.Id);
            A.CallTo(() => _securityService.GetRandomBytes(16)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _securityService.HashPassword(A<string>._, salt)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _userRepository.Insert(A<User>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task CreateUser_WhenEmailExists_ShouldThrow()
        {
            //Arrange
            A.CallTo(() => _userRepository.GetByEmail(A<string>._)).Returns(Task.FromResult<User?>(new User()));
            A.CallTo(() => _userRepository.Insert(A<User>._)).Returns(new User { Id = 1 });

            var salt = new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            A.CallTo(() => _securityService.GetRandomBytes(16)).Returns(salt);
            A.CallTo(() => _securityService.HashPassword(A<string>._, salt)).Returns("hash");

            var newUser = new NewUserDto
            {
                FirstName = "test",
                LastName = "user",
                Email = "test@test.com",
                Password = "password"
            };

            //Act
            var code = async () => await _sut.CreateUser(newUser);

            //Assert
            await Assert.ThrowsAsync<DuplicateAccountException>(code);
        }

        [Fact]
        public async Task GetUser_WhenExists_ShouldReturn()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                FirstName = "test",
                LastName = "user",
                Email = "test@test.com",
            };
            A.CallTo(() => _userRepository.GetById(1)).Returns(Task.FromResult<User?>(user));

            //Act
            var result = await _sut.GetUser(1);

            //Assert
            Assert.Equal(user.Id, result.Id);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task GetUser_WhenAbsent_ShouldThrow()
        {
            // Arrange
            A.CallTo(() => _userRepository.GetById(1)).Returns(Task.FromResult<User?>(null));

            //Act
            var code = async () => await _sut.GetUser(1);

            //Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(code);
        }

        [Fact]
        public async Task VerifyEmail_WhenExists_ShouldUpdate()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                FirstName = "test",
                LastName = "user",
                Email = "test@test.com",
            };
            A.CallTo(() => _userRepository.GetById(1)).Returns(Task.FromResult<User?>(user));

            //Act
            await _sut.VerifyEmail(1);

            //Assert
            Assert.True(user.EmailVerified);
            A.CallTo(() => _userRepository.Update(A<User>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task VerifyEmail_WhenAbsent_ShouldThrow()
        {
            // Arrange
            A.CallTo(() => _userRepository.GetById(1)).Returns(Task.FromResult<User?>(null));

            //Act
            var code = async () => await _sut.VerifyEmail(1);

            //Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(code);
        }
    }
}
