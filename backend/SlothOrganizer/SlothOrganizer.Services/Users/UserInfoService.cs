using AutoMapper;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Domain.Repositories;
using SlothOrganizer.Services.Abstractions.Users;

namespace SlothOrganizer.Services.Users
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserInfoService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task Update(UpdateUserDto updateUserDto)
        {
            if (updateUserDto.AvatarUrl != null)
            {
                await _userRepository.UpdateAvatar(updateUserDto.AvatarUrl, updateUserDto.Id);
            }

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
    }
}
