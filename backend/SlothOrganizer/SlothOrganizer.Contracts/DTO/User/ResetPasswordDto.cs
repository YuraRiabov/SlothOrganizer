﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothOrganizer.Contracts.DTO.User
{
    public class ResetPasswordDto
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
