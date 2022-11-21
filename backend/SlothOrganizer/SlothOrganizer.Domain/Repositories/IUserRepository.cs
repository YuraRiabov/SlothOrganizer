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
        public User GetById(long id);
        public IEnumerable<User> GetAll();
        public User GetByEmail(string email);
        public User Insert(User user);
        public User Update(User user);
        public User Delete(long id);
    }
}
