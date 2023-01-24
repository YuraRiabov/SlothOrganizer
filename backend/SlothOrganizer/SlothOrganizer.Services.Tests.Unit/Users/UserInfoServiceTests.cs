using AutoMapper;
using FakeItEasy;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Domain.Entities;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Utility;
using SlothOrganizer.Services.Users;
using Xunit;

namespace SlothOrganizer.Services.Tests.Unit.Users
{
    public class UserInfoServiceTests
    {
        private readonly UserInfoService _userInfoService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public UserInfoServiceTests()
        {
            _userRepository = A.Fake<IUserRepository>();
            _imageService = A.Fake<IImageService>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<UserMappingProfile>());
            _mapper = config.CreateMapper();

            _userInfoService = new UserInfoService(_userRepository, _mapper, _imageService);
        }
        
        [Fact]
        public async Task Update_WhenFirstNameNotNull_ShouldUpdateFirstName()
        {
            var userUpdate = new UpdateUserDto
            {
                Id = 1,
                FirstName = "Test"
            };

            await _userInfoService.Update(userUpdate);

            A.CallTo(() => _userRepository.UpdateFirstName(userUpdate.FirstName, userUpdate.Id)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Update_WhenLastNameNotNull_ShouldUpdateLastName()
        {
            var userUpdate = new UpdateUserDto
            {
                Id = 1,
                LastName = "Test"
            };

            await _userInfoService.Update(userUpdate);

            A.CallTo(() => _userRepository.UpdateLastName(userUpdate.LastName, userUpdate.Id)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateAvatar_WhenUserExists_ShouldUpdateAndReturn()
        {
            var avatar = GetBytes();
            var fileName = "name";
            var id = 1;
            var user = GetUser();
            A.CallTo(() => _imageService.Upload(avatar, fileName)).Returns("string");
            A.CallTo(() => _userRepository.UpdateAvatar("string", id)).Returns(user);

            var result = await _userInfoService.UpdateAvatar(id, avatar, fileName);

            Assert.Equal(result.Id, user.Id);
            A.CallTo(() => _userRepository.UpdateAvatar("string", id)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateAvatar_WhenUserAbsent_ShouldThrow()
        {
            var avatar = GetBytes();
            var fileName = "name";
            var id = 1;
            A.CallTo(() => _imageService.Upload(avatar, fileName)).Returns("string");
            A.CallTo(() => _userRepository.UpdateAvatar("string", id)).Returns(Task.FromResult<User?>(null));

            var code = async () => await _userInfoService.UpdateAvatar(id, avatar, fileName);

            var exceeption = await Assert.ThrowsAsync<EntityNotFoundException>(code);
            Assert.Equal("No user found with such id", exceeption.Message);
        }

        [Fact]
        public async Task DeleteAvatar_ShouldDelete()
        {
            var userId = 1;

            await _userInfoService.DeleteAvatar(userId);

            A.CallTo(() => _userRepository.UpdateAvatar(null, userId)).MustHaveHappenedOnceExactly();
        }

        private static byte[] GetBytes()
        {
            return new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }

        private static User GetUser()
        {
            return new User
            {
                Id = 1,
                FirstName = "test",
                LastName = "user",
                Email = "test@test.com"
            };
        }
    }
}
