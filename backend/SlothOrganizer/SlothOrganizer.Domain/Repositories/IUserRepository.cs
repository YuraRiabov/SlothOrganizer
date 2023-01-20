using SlothOrganizer.Domain.Entities;

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
        Task<User?> VerifyEmail(string email, int code);
        Task<User?> Get(string email, int verificationCode);
        Task UpdateAvatar(string? avatar, long id);
        Task UpdateFirstName(string firstName, long id);
        Task UpdateLastName(string lastName, long id);
    }
}
