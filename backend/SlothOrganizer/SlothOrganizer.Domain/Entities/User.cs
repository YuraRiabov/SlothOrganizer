﻿namespace SlothOrganizer.Domain.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool EmailVerified { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
