using System.Net;
using SlothOrganizer.Web.Tests.Integration.Base;
using SlothOrganizer.Web.Tests.Integration.Setup;
using Xunit;

namespace SlothOrganizer.Web.Tests.Integration.Tests
{
    [Collection("DbUsingTests")]
    public class UserTests: TestBase
    {
        private const string ControllerRoute = "users";

        [Fact]
        public async Task ResetPassword_WhenValidTokenAndUserExists_ShouldReset()
        {
            await AddAuthorizationHeader();
            var dto = DtoProvider.GetResetPasswordDto();

            var response = await Client.PutAsync($"{ControllerRoute}/resetPassword", GetStringContent(dto));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var login = DtoProvider.GetLogin();
            var oldAuthResult = await Client.PostAsync("auth/signIn", GetStringContent(login));
            Assert.Equal(HttpStatusCode.Forbidden, oldAuthResult.StatusCode);

            login.Password = dto.Password;
            var newAuthResult = await Client.PostAsync("auth/signIn", GetStringContent(login));
            Assert.Equal(HttpStatusCode.OK, newAuthResult.StatusCode);
        }

        [Fact]
        public async Task ResetPassword_WhenValidTokenAndUserAbsent_NotFound()
        {
            await AddAuthorizationHeader();
            var dto = DtoProvider.GetResetPasswordDto();
            dto.Email = "test@testing.com";

            var response = await Client.PutAsync($"{ControllerRoute}/resetPassword", GetStringContent(dto));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            var login = DtoProvider.GetLogin();
            var authResult = await Client.PostAsync("auth/signIn", GetStringContent(login));
            Assert.Equal(HttpStatusCode.OK, authResult.StatusCode);
        }

        [Fact]
        public async Task ResetPassword_WhenInvalidToken_Unauthorized()
        {
            var dto = DtoProvider.GetResetPasswordDto();

            var response = await Client.PutAsync($"{ControllerRoute}/resetPassword", GetStringContent(dto));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task ResetPassword_WhenInvalidEmail_BadRequest()
        {
            await AddAuthorizationHeader();
            var dto = DtoProvider.GetResetPasswordDto();
            dto.Email = "testtesting.com";

            var response = await Client.PutAsync($"{ControllerRoute}/resetPassword", GetStringContent(dto));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("short1")]
        [InlineData("loooooooooooooong1")]
        [InlineData("invalidpattern")]
        [InlineData("11111111111")]
        public async Task ResetPassword_WhenInvalidPassword_BadRequest(string password)
        {
            await AddAuthorizationHeader();
            var dto = DtoProvider.GetResetPasswordDto();
            dto.Password = password;

            var response = await Client.PutAsync($"{ControllerRoute}/resetPassword", GetStringContent(dto));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
