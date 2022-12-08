using System.Net;
using FakeItEasy;
using SlothOrganizer.Contracts.DTO.Auth;
using SlothOrganizer.Contracts.DTO.User;
using SlothOrganizer.Persistence.Repositories;
using SlothOrganizer.Web.Tests.Integration.Base;
using Xunit;

namespace SlothOrganizer.Web.Tests.Integration.Tests
{
    [Collection("DbUsingTests")]
    public class AuthTests : TestBase
    {
        private const string ControllerRoute = "auth";
        [Fact]
        public async Task SignUp_WhenValidRequest_ShouldReturn()
        {
            var newUser = GetNewUser();

            var response = await Client.PostAsync($"{ControllerRoute}/signup", GetStringContent(newUser));
            var result = await GetResponse<UserDto>(response);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(newUser.FirstName, result.FirstName);
            Assert.Equal(newUser.LastName, result.LastName);
            Assert.Equal(newUser.Email, result.Email);
            Assert.False(result.EmailVerified);

            Assert.Single(await new UserRepository(Context).GetAll());
            Assert.Single(await new VerificationCodeRepository(Context).Get(result.Id));
        }

        [Theory]
        [InlineData("Y", "Riabov", "test@test.com", "passw0rd")]
        [InlineData("Yura", "R", "test@test.com", "passw0rd")]
        [InlineData("Yuraaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Riabov", "test@test.com", "passw0rd")]
        [InlineData("Yura", "Yuraaaaaaaaaaaaaaaaaaaaaaaaaaaa", "test@test.com", "passw0rd")]
        [InlineData("Yura", "Riabov", "testtest.com", "passw0rd")]
        [InlineData("Yura", "Riabov", "test@test.com", "password")]
        [InlineData("Yura", "Riabov", "test@test.com", "passw0r")]
        [InlineData("Yura", "Riabov", "test@test.com", "passw0rdddddddddd")]
        public async Task SignUp_WhenInvalidData_BadRequest(string firstName, string lastName, string email, string password)
        {
            var newUser = new {firstName, lastName, email, password};

            var response = await Client.PostAsync($"{ControllerRoute}/signup", GetStringContent(newUser));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task SignUp_WhenEmailExists_BadRequest()
        {
            var newUser = GetNewUser();

            await Client.PostAsync($"{ControllerRoute}/signup", GetStringContent(newUser));
            var response = await Client.PostAsync($"{ControllerRoute}/signup", GetStringContent(newUser));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Single(await new UserRepository(Context).GetAll());
        }

        [Fact]
        public async Task VerifyEmail_WhenValidData_ShouldReturnTokenAndVerify()
        {
            A.CallTo(() => RandomService.GetRandomNumber(6)).Returns(111111);
            A.CallTo(() => DateTimeService.Now()).Returns(DateTime.Now.AddHours(1));
            var newUser = GetNewUser();

            var signUpResponse = await Client.PostAsync($"{ControllerRoute}/signup", GetStringContent(newUser));
            var user = await GetResponse<UserDto>(signUpResponse);

            var verificationCode = GetVerificationCode(user.Id);

            var verificationResponse = await Client.PutAsync($"{ControllerRoute}/verifyEmail", GetStringContent(verificationCode));
            var result = await GetResponse<TokenDto>(verificationResponse);

            Assert.Equal(HttpStatusCode.OK, verificationResponse.StatusCode);
            Assert.Single(await new RefreshTokenRepository(Context).Get(user.Email));
            var modifiedUser = await new UserRepository(Context).Get(user.Id);
            Assert.True(modifiedUser.EmailVerified);
        }

        [Fact]
        public async Task VerifyEmail_WhenExpired_Forbidden()
        {
            A.CallTo(() => RandomService.GetRandomNumber(6)).Returns(111111);
            A.CallTo(() => DateTimeService.Now()).Returns(DateTime.Now.AddMinutes(-2));
            var newUser = GetNewUser();

            var signUpResponse = await Client.PostAsync($"{ControllerRoute}/signup", GetStringContent(newUser));
            var user = await GetResponse<UserDto>(signUpResponse);

            var verificationCode = GetVerificationCode(user.Id);

            var verificationResponse = await Client.PutAsync($"{ControllerRoute}/verifyEmail", GetStringContent(verificationCode));

            Assert.Equal(HttpStatusCode.Forbidden, verificationResponse.StatusCode);
        }

        [Fact]
        public async Task VerifyEmail_NoMatching_Forbidden()
        {
            VerificationCodeDto verificationCode = GetVerificationCode(1);

            var verificationResponse = await Client.PutAsync($"{ControllerRoute}/verifyEmail", GetStringContent(verificationCode));

            Assert.Equal(HttpStatusCode.Forbidden, verificationResponse.StatusCode);
        }

        private static VerificationCodeDto GetVerificationCode(long userId)
        {
            return new VerificationCodeDto
            {
                VerificationCode = 111111,
                UserId = userId
            };
        }

        private NewUserDto GetNewUser()
        {
            return new NewUserDto
            {
                FirstName = "Yura",
                LastName = "Riabov",
                Email = "test@test.com",
                Password = "passw0rd"
            };
        }
    }
}
