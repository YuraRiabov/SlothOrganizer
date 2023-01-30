using FakeItEasy;
using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;
using System.Net.Http.Headers;
using SlothOrganizer.Web.Tests.Integration.Setup.Providers;

namespace SlothOrganizer.Web.Tests.Integration.Tests.Base
{
    public class AuthorizedTestBase : TestBase
    {
        protected async Task AddAuthorizationHeader()
        {
            var auth = await SetupVerifiedUser();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth.Token.AccessToken);
        }

        protected async Task<UserAuthDto> SetupVerifiedUser()
        {
            var provider = new AuthDtoProvider();
            var user = await SetupUser();
            var verificationCode = provider.GetVerificationCode(user.Email);
            var verificationResponse = await Client.PutAsync("auth/verify-email", GetStringContent(verificationCode));
            return await GetResponse<UserAuthDto>(verificationResponse);
        }

        protected async Task<UserDto> SetupUser()
        {
            var provider = new AuthDtoProvider();
            A.CallTo(() => RandomService.GetRandomNumber(6)).Returns(111111);
            A.CallTo(() => DateTimeService.Now()).Returns(DateTime.Now.AddHours(1));
            var newUser = provider.GetNewUser();
            var signUpResponse = await Client.PostAsync("auth/sign-up", GetStringContent(newUser));
            return await GetResponse<UserDto>(signUpResponse);
        }
    }
}
