using AutoMapper;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Domain.Exceptions;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Users;
using SlothOrganizer.Services.Abstractions.Utility;

namespace SlothOrganizer.Services.Users
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        public UserInfoService(IUserRepository userRepository, IMapper mapper, IImageService imageService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task Update(UpdateUserDto updateUserDto)
        {
            var updateTasks = new List<Task>();
            if (updateUserDto.FirstName != null)
            {
                updateTasks.Add(_userRepository.UpdateFirstName(updateUserDto.FirstName, updateUserDto.Id));
            }

            if (updateUserDto.LastName != null)
            {
                updateTasks.Add(_userRepository.UpdateLastName(updateUserDto.LastName, updateUserDto.Id));
            }
            await Task.WhenAll(updateTasks);
        }

        public async Task DeleteAvatar(long userId)
        {
            await _userRepository.UpdateAvatar(null, userId);
        }

        public async Task<UserDto> UpdateAvatar(long userId, byte[] avatar, string fileName)
        {
            var url = await _imageService.Upload(avatar, fileName);
            var user = await _userRepository.UpdateAvatar(url, userId);
            return _mapper.Map<UserDto>(user);
        }
    }
}
