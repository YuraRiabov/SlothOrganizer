using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlothOrganizer.Domain.Entities;

namespace SlothOrganizer.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<User?> GetById(long id);
        public Task<IEnumerable<User>> GetAll();
        public Task<User?> GetByEmail(string email);
        public Task<User> Insert(User user);
        public Task Update(User user);
        public Task Delete(long id);
    }
}
