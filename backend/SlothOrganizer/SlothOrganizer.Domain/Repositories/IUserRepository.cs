﻿using SlothOrganizer.Domain.Entities;

namespace SlothOrganizer.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<User?> Get(long id);
        public Task<IEnumerable<User>> GetAll();
        public Task<User?> Get(string email);
        public Task<User> Insert(User user);
        public Task Update(User user);
        public Task Delete(long id);
    }
}
