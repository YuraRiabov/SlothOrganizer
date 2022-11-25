using SlothOrganizer.Domain.Entities;

namespace SlothOrganizer.Domain.Repositories
{
    public interface IVerificationCodeRepository
    {
        Task<IEnumerable<VerificationCode>> GetByUserId(long userId);

        Task<VerificationCode> Insert(VerificationCode verificationCode);
    }
}
