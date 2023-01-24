using System.Net;
using FakeItEasy;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Persistence.Repositories;
using SlothOrganizer.Web.Tests.Integration.Base;
using SlothOrganizer.Web.Tests.Integration.Setup;

namespace SlothOrganizer.Web.Tests.Integration.Tests
{
    [UsesVerify]
    [Collection("DbUsingTests")]
    public class UserInfoTests : TestBase
    {
        private const string ControllerRoute = "users-info";

        [Fact]
        public async Task Update_WhenFirstNameNotNull_ShouldUpdate()
        {
            await AddAuthorizationHeader();
            var userUpdate = DtoProvider.GetUserUpdate();
            userUpdate.LastName = null;

            var response = await Client.PutAsync($"{ControllerRoute}", GetStringContent(userUpdate));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var user = await new UserRepository(Context).Get(userUpdate.Id);
            Assert.Equal(user.FirstName, userUpdate.FirstName);
            Assert.NotNull(user.LastName);
        }

        [Fact]
        public async Task Update_WhenLastNameNotNull_ShouldUpdate()
        {
            await AddAuthorizationHeader();
            var userUpdate = DtoProvider.GetUserUpdate();
            userUpdate.FirstName = null;

            var response = await Client.PutAsync($"{ControllerRoute}", GetStringContent(userUpdate));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var user = await new UserRepository(Context).Get(userUpdate.Id);
            Assert.Equal(user.LastName, userUpdate.LastName);
            Assert.NotNull(user.FirstName);
        }

        [Fact]
        public async Task DeleteAvatar_ShouldDelete()
        {
            await AddAuthorizationHeader();
            var userId = 1;

            var response = await Client.DeleteAsync($"{ControllerRoute}/{userId}/avatar");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var user = await new UserRepository(Context).Get(userId);
            Assert.Null(user.AvatarUrl);
        }
    }
}
