using SlothOrganizer.Contracts.DTO.User;

namespace SlothOrganizer.Contracts.DTO.Auth
{
    public class UserAuthDto
    {
        public UserDto User { get; set; }
        public TokenDto? Token { get; set; }
    }
}
