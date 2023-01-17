using System.Net;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Persistence.Repositories;
using SlothOrganizer.Web.Tests.Integration.Base;
using SlothOrganizer.Web.Tests.Integration.Setup;
using Xunit;

namespace SlothOrganizer.Web.Tests.Integration.Tests
{
    [Collection("DbUsingTests")]
    public class AuthTests : TestBase
    {
        private const string ControllerRoute = "auth";

        [Fact]
        public async Task SendVerificationCode_ShouldSend()
        {
            var user = await SetupUser();

            var response = await Client.PostAsync($"{ControllerRoute}/send-code/{user.Email}", null);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var codes = await new VerificationCodeRepository(Context).Get(user.Email);
            Assert.Single(codes);
        }

        [Fact]
        public async Task RefreshToken_WhenValid_ShouldRefresh()
        {
            var userAuth = await SetupVerifiedUser();

            var response = await Client.PutAsync($"{ControllerRoute}/refresh-token", GetStringContent(userAuth.Token));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var tokens = await new RefreshTokenRepository(Context).Get(DtoProvider.GetNewUser().Email);
            Assert.Equal(2, tokens.Count());
        }

        [Fact]
        public async Task RefreshToken_WhenInvalid_Forbidden()
        {
            A.CallTo(() => RandomService.GetRandomNumber(6)).Returns(111111);
            A.CallTo(() => DateTimeService.Now()).Returns(DateTime.Now.AddHours(1));

            var auth = await SetupVerifiedUser();
            var token = new { accessToken = "access", refreshToken = "refresh" };

            var response = await Client.PutAsync($"{ControllerRoute}/refresh-token", GetStringContent(token));

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task SendPasswordReset_ShouldSend()
        {
            var auth = await SetupVerifiedUser();

            var response = await Client.PostAsync($"{ControllerRoute}/send-password-reset/{auth.User.Email}", null);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            A.CallTo(() => EmailSerivce.SendEmail(auth.User.Email, "Follow the link to reset your password", A<string>._)).MustHaveHappenedOnceExactly();
        }
    }
}
