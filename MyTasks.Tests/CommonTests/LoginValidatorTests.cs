using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyTasks.Common;
using MyTasks.Models.Models;
using MyTasks.Repositories.DTOS;
using MyTasks.Repositories.Interfaces.ILoginRepository;

namespace MyTasks.Tests
{
    public class LoginValidatorTests
    {
        private readonly ILoginRepository _fakeLoginRepo;
        private readonly IConfiguration _fakeConfig;
        private readonly IHttpContextAccessor _fakeHttpContext;
        private readonly LoginValidator _sut;

        public LoginValidatorTests()
        {
            _fakeLoginRepo = A.Fake<ILoginRepository>();

            // fake config with JWT settings
            var inMemorySettings = new Dictionary<string, string> {
                {"Jwt:Key", "supersecretkey1234567890"},
                {"Jwt:Issuer", "testissuer"},
                {"Jwt:Audience", "testaudience"},
                {"Jwt:ExpireMinutes", "60"}
            };
            _fakeConfig = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _fakeHttpContext = A.Fake<IHttpContextAccessor>();
            _sut = new LoginValidator(_fakeLoginRepo, _fakeConfig, _fakeHttpContext);
        }

        [Fact]
        public async Task ValidateLogin_ShouldReturn404_WhenLoginNotFound()
        {
            // Arrange
            var request = new LoginRequest("notfound", "pass");
            A.CallTo(() => _fakeLoginRepo.GetUserLoginDataByUserNameAsync(request.Username))
                .Returns(Task.FromResult<LoginModel?>(null));

            // Act
            var result = await _sut.ValidateLogin(request);

            // Assert
            var json = Assert.IsType<JsonResult>(result);
            Assert.Equal(404, json.StatusCode);
        }

        [Fact]
        public async Task ValidateLogin_ShouldReturn404_WhenUserNotFound()
        {
            // Arrange
            var login = new LoginModel { UserId = Guid.NewGuid(), Username = "user", PasswordHash = "hash", FakeUser = true };
            A.CallTo(() => _fakeLoginRepo.GetUserLoginDataByUserNameAsync(login.Username))
                .Returns(Task.FromResult<LoginModel?>(login));

            A.CallTo(() => _fakeLoginRepo.GetUserDataByIdAsync(login.UserId))
                .Returns(Task.FromResult<UserModel?>(null));

            var request = new LoginRequest("user", "hash");

            // Act
            var result = await _sut.ValidateLogin(request);

            // Assert
            var json = Assert.IsType<JsonResult>(result);
            Assert.Equal(404, json.StatusCode);
        }

        [Fact]
        public async Task ValidateLogin_ShouldReturn401_WhenPasswordInvalid()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var login = new LoginModel { UserId = userId, Username = "user", PasswordHash = "hash", FakeUser = true };
            var user = new UserModel { Id = userId, FullName = "Test User", IsDeleted = false };

            A.CallTo(() => _fakeLoginRepo.GetUserLoginDataByUserNameAsync(login.Username))
                .Returns(Task.FromResult<LoginModel?>(login));
            A.CallTo(() => _fakeLoginRepo.GetUserDataByIdAsync(userId))
                .Returns(Task.FromResult<UserModel?>(user));

            var request = new LoginRequest("user", "wrongpass");

            // Act
            var result = await _sut.ValidateLogin(request);

            // Assert
            var json = Assert.IsType<JsonResult>(result);
            Assert.Equal(401, json.StatusCode);
        }

        [Fact]
        public async Task ValidateLogin_ShouldReturn500_WhenNoHttpContextResponse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var login = new LoginModel { UserId = userId, Username = "user", PasswordHash = "pass", FakeUser = true };
            var user = new UserModel { Id = userId, FullName = "Test User", IsDeleted = false };

            A.CallTo(() => _fakeLoginRepo.GetUserLoginDataByUserNameAsync(login.Username))
                .Returns(Task.FromResult<LoginModel?>(login));
            A.CallTo(() => _fakeLoginRepo.GetUserDataByIdAsync(userId))
                .Returns(Task.FromResult<UserModel?>(user));

            var request = new LoginRequest("user", "pass");
            A.CallTo(() => _fakeHttpContext.HttpContext).Returns((HttpContext?)null);

            // Act
            var result = await _sut.ValidateLogin(request);

            // Assert
            var json = Assert.IsType<JsonResult>(result);
            Assert.Equal(500, json.StatusCode);
        }
    }
}