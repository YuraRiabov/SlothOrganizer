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

        [Fact]
        public async Task ResendVerificationCode_WhenUserExists_ShouldSend()
        {
            var user = await SetupUser();

            var response = await Client.PostAsync($"{ControllerRoute}/resendCode/{user.Id}", null);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var codes = await new VerificationCodeRepository(Context).Get(user.Id);
            Assert.Equal(2, codes.Count());
        }

        [Fact]
        public async Task ResendVerificationCode_WhenInvalidId_NotFound()
        {
            var response = await Client.PostAsync($"{ControllerRoute}/resendCode/1", null);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task SignIn_WhenValidDataAndVerified_ShouldReturnWithToken()
        {
            await SetupVerifiedUser();

            var loginDto = GetLogin();

            var response = await Client.PostAsync($"{ControllerRoute}/signIn", GetStringContent(loginDto));
            var userAuth = await GetResponse<UserAuthDto>(response);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(userAuth.Token);
            Assert.Equal(GetNewUser().Email, userAuth.User.Email);
            var tokens = await new RefreshTokenRepository(Context).Get(GetNewUser().Email);
            Assert.Equal(2, tokens.Count());
        }

        [Fact]
        public async Task SignIn_WhenValidDataAndNotVerified_ShouldReturnWithoutToken()
        {
            var user = await SetupUser();

            var loginDto = GetLogin();

            var response = await Client.PostAsync($"{ControllerRoute}/signIn", GetStringContent(loginDto));
            var userAuth = await GetResponse<UserAuthDto>(response);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Null(userAuth.Token);
            Assert.Equal(user.Email, userAuth.User.Email);
            Assert.Empty(await new RefreshTokenRepository(Context).Get(user.Email));
        }

        [Fact]
        public async Task SignIn_WhenInvalidEmail_NotFound()
        {
            var loginDto = new { email = "test@testing.com", password = "passw0rd" };

            var response = await Client.PostAsync($"{ControllerRoute}/signIn", GetStringContent(loginDto));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task SignIn_WhenInvalidPassword_Forbidden()
        {
            await SetupUser();

            var loginDto = new { email = "test@test.com", password = "passw0rd1" };

            var response = await Client.PostAsync($"{ControllerRoute}/signIn", GetStringContent(loginDto));

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Theory]
        [InlineData("test@test.com", "password")]
        [InlineData("testtest.com", "passw0rd")]
        [InlineData("test@test.com", "passw0r")]
        [InlineData("test@test.com", "passw0rdddddddddd")]
        public async Task SignIn_WhenIvalidData_BadRequest(string email, string password)
        {
            var loginDto = new { email, password };
            var response = await Client.PostAsync($"{ControllerRoute}/signIn", GetStringContent(loginDto));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task RefreshToken_WhenValid_ShouldRefresh()
        {
            var token = await SetupVerifiedUser();

            var response = await Client.PutAsync($"{ControllerRoute}/refreshToken", GetStringContent(token));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var tokens = await new RefreshTokenRepository(Context).Get(GetNewUser().Email);
            Assert.Equal(2, tokens.Count());
        }

        [Fact]
        public async Task RefreshToken_WhenInvalid_Forbidden()
        {
            A.CallTo(() => RandomService.GetRandomNumber(6)).Returns(111111);
            A.CallTo(() => DateTimeService.Now()).Returns(DateTime.Now.AddHours(1));
            var newUser = GetNewUser();

            var signUpResponse = await Client.PostAsync($"{ControllerRoute}/signup", GetStringContent(newUser));
            var user = await GetResponse<UserDto>(signUpResponse);

            var verificationCode = GetVerificationCode(user.Id);

            var verificationResponse = await Client.PutAsync($"{ControllerRoute}/verifyEmail", GetStringContent(verificationCode));
            var token = new { accessToken = "access", refreshToken = "refresh" };

            var response = await Client.PutAsync($"{ControllerRoute}/refreshToken", GetStringContent(token));

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        private async Task<TokenDto> SetupVerifiedUser()
        {
            var user = await SetupUser();
            var verificationCode = GetVerificationCode(user.Id);
            var verificationResponse = await Client.PutAsync($"{ControllerRoute}/verifyEmail", GetStringContent(verificationCode));
            return await GetResponse<TokenDto>(verificationResponse);
        }

        private async Task<UserDto> SetupUser()
        {
            A.CallTo(() => RandomService.GetRandomNumber(6)).Returns(111111);
            A.CallTo(() => DateTimeService.Now()).Returns(DateTime.Now.AddHours(1));
            var newUser = GetNewUser();
            var signUpResponse = await Client.PostAsync($"{ControllerRoute}/signup", GetStringContent(newUser));
            return await GetResponse<UserDto>(signUpResponse);
        }

        private LoginDto GetLogin()
        {
            return new LoginDto
            {
                Email = "test@test.com",
                Password = "passw0rd"
            };
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
