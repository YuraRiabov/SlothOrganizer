using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlothOrganizer.Contracts.DTO.User;

namespace SlothOrganizer.Contracts.DTO.Auth
{
    public class UserAuthDto
    {
        public UserDto User { get; set; } = null!;
        public TokenDto? Token { get; set; }
    }
}
