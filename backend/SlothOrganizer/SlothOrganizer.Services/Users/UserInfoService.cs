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
        public UserInfoService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task Update(UpdateUserDto updateUserDto)
        {
            if (updateUserDto.FirstName != null)
            {
                await _userRepository.UpdateFirstName(updateUserDto.FirstName, updateUserDto.Id);
            }

            if (updateUserDto.LastName != null)
            {
                await _userRepository.UpdateLastName(updateUserDto.LastName, updateUserDto.Id);
            }
        }

        public async Task DeleteAvatar(long userId)
        {
            await _userRepository.UpdateAvatar(null, userId);
        }

        public async Task<UserDto> UpdateAvatar(long userId, byte[] avatar, string fileName)
        {
            var url = await _imageService.Upload(avatar, fileName);
            var user = await _userRepository.UpdateAvatar(url, userId);
            if (user is null)
            {
                throw new EntityNotFoundException("No user found with such id");
            }
            return _mapper.Map<UserDto>(user);
        }
    }
}
