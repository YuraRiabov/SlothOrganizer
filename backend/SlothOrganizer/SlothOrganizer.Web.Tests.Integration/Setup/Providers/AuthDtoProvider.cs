using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;

namespace SlothOrganizer.Web.Tests.Integration.Setup.Providers
{
    public class AuthDtoProvider
    {
        public VerificationCodeDto GetVerificationCode(string email)
        {
            return new VerificationCodeDto
            {
                VerificationCode = 111111,
                Email = email
            };
        }

        public NewUserDto GetNewUser()
        {
            return new NewUserDto
            {
                FirstName = "Yura",
                LastName = "Riabov",
                Email = "test@test.com",
                Password = "passw0rd"
            };
        }

        public LoginDto GetLogin()
        {
            return new LoginDto
            {
                Email = "test@test.com",
                Password = "passw0rd"
            };
        }

        public ResetPasswordDto GetResetPasswordDto()
        {
            return new ResetPasswordDto
            {
                Email = "test@test.com",
                Password = "newpassw0rd",
                Code = "111111"
            };
        }
    }
}
