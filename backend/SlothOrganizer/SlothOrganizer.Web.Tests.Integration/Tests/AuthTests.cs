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
        public async Task SignUp_WhenValidRequest_ShouldReturn()
        {
            var newUser = DtoProvider.GetNewUser();

            var response = await Client.PostAsync($"{ControllerRoute}/sign-up", GetStringContent(newUser));
            var result = await GetResponse<UserDto>(response);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(newUser.FirstName, result.FirstName);
            Assert.Equal(newUser.LastName, result.LastName);
            Assert.Equal(newUser.Email, result.Email);
            Assert.False(result.EmailVerified);

            Assert.Single(await new UserRepository(Context).GetAll());
            Assert.Single(await new VerificationCodeRepository(Context).Get(result.Email));
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

            var response = await Client.PostAsync($"{ControllerRoute}/sign-up", GetStringContent(newUser));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task SignUp_WhenEmailExists_BadRequest()
        {
            var newUser = DtoProvider.GetNewUser();

            await Client.PostAsync($"{ControllerRoute}/sign-up", GetStringContent(newUser));
            var response = await Client.PostAsync($"{ControllerRoute}/sign-up", GetStringContent(newUser));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Single(await new UserRepository(Context).GetAll());
        }

        [Fact]
        public async Task VerifyEmail_WhenValidData_ShouldReturnTokenAndVerify()
        {
            A.CallTo(() => RandomService.GetRandomNumber(6)).Returns(111111);
            A.CallTo(() => DateTimeService.Now()).Returns(DateTime.Now.AddHours(1));
            var newUser = DtoProvider.GetNewUser();

            var signUpResponse = await Client.PostAsync($"{ControllerRoute}/sign-up", GetStringContent(newUser));
            var user = await GetResponse<UserDto>(signUpResponse);

            var verificationCode = DtoProvider.GetVerificationCode(user.Email);

            var verificationResponse = await Client.PutAsync($"{ControllerRoute}/verify-email", GetStringContent(verificationCode));
            var result = await GetResponse<UserAuthDto>(verificationResponse);

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
            var newUser = DtoProvider.GetNewUser();

            var signUpResponse = await Client.PostAsync($"{ControllerRoute}/sign-up", GetStringContent(newUser));
            var user = await GetResponse<UserDto>(signUpResponse);

            var verificationCode = DtoProvider.GetVerificationCode(user.Email);

            var verificationResponse = await Client.PutAsync($"{ControllerRoute}/verify-email", GetStringContent(verificationCode));

            Assert.Equal(HttpStatusCode.Forbidden, verificationResponse.StatusCode);
        }

        [Fact]
        public async Task VerifyEmail_NoMatching_Forbidden()
        {
            VerificationCodeDto verificationCode = DtoProvider.GetVerificationCode("test@test.com");

            var verificationResponse = await Client.PutAsync($"{ControllerRoute}/verify-email", GetStringContent(verificationCode));

            Assert.Equal(HttpStatusCode.Forbidden, verificationResponse.StatusCode);
        }

        [Fact]
        public async Task ResendVerificationCode_WhenUserExists_ShouldSend()
        {
            var user = await SetupUser();

            var response = await Client.PostAsync($"{ControllerRoute}/resend-code/{user.Email}", null);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var codes = await new VerificationCodeRepository(Context).Get(user.Email);
            Assert.Equal(2, codes.Count());
        }

        [Fact]
        public async Task ResendVerificationCode_WhenInvalidId_NotFound()
        {
            var response = await Client.PostAsync($"{ControllerRoute}/resend-code/1", null);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task SignIn_WhenValidDataAndVerified_ShouldReturnWithToken()
        {
            var userAuth = await SetupVerifiedUser();

            var loginDto = DtoProvider.GetLogin();

            var response = await Client.PostAsync($"{ControllerRoute}/sign-in", GetStringContent(loginDto));
            var result = await GetResponse<UserAuthDto>(response);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result.Token);
            Assert.Equal(userAuth.User.Email, result.User.Email);
            var tokens = await new RefreshTokenRepository(Context).Get(result.User.Email);
            Assert.Equal(2, tokens.Count());
        }

        [Fact]
        public async Task SignIn_WhenValidDataAndNotVerified_ShouldReturnWithoutToken()
        {
            var user = await SetupUser();

            var loginDto = DtoProvider.GetLogin();

            var response = await Client.PostAsync($"{ControllerRoute}/sign-in", GetStringContent(loginDto));
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

            var response = await Client.PostAsync($"{ControllerRoute}/sign-in", GetStringContent(loginDto));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task SignIn_WhenInvalidPassword_Forbidden()
        {
            await SetupUser();

            var loginDto = new { email = "test@test.com", password = "passw0rd1" };

            var response = await Client.PostAsync($"{ControllerRoute}/sign-in", GetStringContent(loginDto));

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
            var response = await Client.PostAsync($"{ControllerRoute}/sign-in", GetStringContent(loginDto));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
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
            var newUser = DtoProvider.GetNewUser();

            var signUpResponse = await Client.PostAsync($"{ControllerRoute}/sign-up", GetStringContent(newUser));
            var user = await GetResponse<UserDto>(signUpResponse);

            var verificationCode = DtoProvider.GetVerificationCode(user.Email);

            var verificationResponse = await Client.PutAsync($"{ControllerRoute}/verify-email", GetStringContent(verificationCode));
            var token = new { accessToken = "access", refreshToken = "refresh" };

            var response = await Client.PutAsync($"{ControllerRoute}/refresh-token", GetStringContent(token));

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task ResetPassword_WhenValid_ShouldReset()
        {
            await SetupVerifiedUser();
            var dto = DtoProvider.GetResetPasswordDto();
            A.CallTo(() => CryptoService.Decrypt(dto.Code)).Returns(dto.Code);
            A.CallTo(() => CryptoService.Decrypt(dto.Email)).Returns(dto.Email);

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
        public async Task ResetPassword_WhenUserAbsent_NotFound()
        {
            await SetupVerifiedUser();
            var dto = DtoProvider.GetResetPasswordDto();
            A.CallTo(() => CryptoService.Decrypt(dto.Code)).Returns(dto.Code);
            A.CallTo(() => CryptoService.Decrypt(dto.Email)).Returns("invalid email");

            var response = await Client.PutAsync($"{ControllerRoute}/resetPassword", GetStringContent(dto));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            var login = DtoProvider.GetLogin();
            var authResult = await Client.PostAsync("auth/signIn", GetStringContent(login));
            Assert.Equal(HttpStatusCode.OK, authResult.StatusCode);
        }

        [Fact]
        public async Task ResetPassword_WhenInvalidCode_Forbidden()
        {
            await SetupVerifiedUser();
            var dto = DtoProvider.GetResetPasswordDto();
            A.CallTo(() => CryptoService.Decrypt(dto.Code)).Returns("not a number");

            var response = await Client.PutAsync($"{ControllerRoute}/resetPassword", GetStringContent(dto));

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Theory]
        [InlineData("short1")]
        [InlineData("loooooooooooooong1")]
        [InlineData("invalidpattern")]
        [InlineData("11111111111")]
        public async Task ResetPassword_WhenInvalidPassword_BadRequest(string password)
        {
            await SetupVerifiedUser();
            var dto = DtoProvider.GetResetPasswordDto();
            dto.Password = password;

            var response = await Client.PutAsync($"{ControllerRoute}/resetPassword", GetStringContent(dto));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task SendPasswordReset_WhenUserExists_ShouldSend()
        {
            var auth = await SetupVerifiedUser();

            var response = await Client.PostAsync($"{ControllerRoute}/sendPasswordReset/{auth.User.Email}", null);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            A.CallTo(() => EmailSerivce.SendEmail(auth.User.Email, "Follow the link to reset your password", A<string>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task SendPasswordReset_WhenUserAbsent_NotFound()
        {
            var response = await Client.PostAsync($"{ControllerRoute}/sendPasswordReset/test@test.com", null);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
