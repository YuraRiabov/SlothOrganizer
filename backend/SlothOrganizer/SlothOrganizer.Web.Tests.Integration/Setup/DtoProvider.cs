using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;

namespace SlothOrganizer.Web.Tests.Integration.Setup
{
    public static class DtoProvider
    {
        public static VerificationCodeDto GetVerificationCode(string email)
        {
            return new VerificationCodeDto
            {
                VerificationCode = 111111,
                Email = email
            };
        }

        public static NewUserDto GetNewUser()
        {
            return new NewUserDto
            {
                FirstName = "Yura",
                LastName = "Riabov",
                Email = "test@test.com",
                Password = "passw0rd"
            };
        }

        public static LoginDto GetLogin()
        {
            return new LoginDto
            {
                Email = "test@test.com",
                Password = "passw0rd"
            };
        }

        public static ResetPasswordDto GetResetPasswordDto()
        {
            return new ResetPasswordDto
            {
                Email = "test@test.com",
                Password = "newpassw0rd"
            };
        }
    }
}
