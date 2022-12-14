using SlothOrganizer.Domain.Entities;

namespace SlothOrganizer.Domain.Repositories
{
    public interface IVerificationCodeRepository
    {
        Task<IEnumerable<VerificationCode>> Get(string userEmail);

        Task<VerificationCode> Insert(VerificationCode verificationCode, string UserEMail);
    }
}
