using FakeItEasy;
using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;
using MyTasks.Repositories.Repositories.LoginRepository;

namespace MyTasks.Tests
{
    //for tests use FakeItEasy
    public class LoginRepositoryTests
    {
        private readonly IDbRepository<LoginModel> _fakeLoginRepo;
        private readonly IDbRepository<UserModel> _fakeUsernRepo;
        private readonly LoginRepository _sut;

        public LoginRepositoryTests()
        {
            _fakeLoginRepo = A.Fake<IDbRepository<LoginModel>>();
            _fakeUsernRepo = A.Fake<IDbRepository<UserModel>>();
            _sut = new LoginRepository(_fakeLoginRepo, _fakeUsernRepo);
        }

        [Fact]
        public async Task GetUserLoginDataById_ShouldReturnData_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expectedUser = new LoginModel { Id = userId, Username = "testuser" };

            A.CallTo(() => _fakeLoginRepo.GetByIdAsync(userId))
            .Returns(Task.FromResult(expectedUser!));

            // Act
            var result = await _sut.GetUserLoginDataByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Id, result.Id);
            Assert.Equal(expectedUser.Username, result.Username);
        }

        [Fact]
        public async Task GetUserLoginDataById_ShouldReturnNull_WhenUserDoesNotExists()
        {
            // Arrange
            var userId = Guid.NewGuid();

            A.CallTo(() => _fakeLoginRepo.GetByIdAsync(userId))
            .Returns(Task.FromResult<LoginModel?>(null));

            // Act
            var result = await _sut.GetUserLoginDataByIdAsync(userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserLoginDataByUserName_ShouldReturnData_WhenUserExists()
        {
            // Arrange
            var expectedUser = new LoginModel {Username = "testuser" };

            A.CallTo(() => _fakeLoginRepo.GetByUserNameAsync<LoginModel>(expectedUser.Username))
            .Returns(Task.FromResult(expectedUser!));

            // Act
            var result = await _sut.GetUserLoginDataByUserNameAsync(expectedUser.Username);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Username, result.Username);
        }

        [Fact]
        public async Task GetUserLoginDataByUserName_ShouldReturnNull_WhenUserDoesNotExists()
        {
            // Arrange
            var userName = "testUserName";

            A.CallTo(() => _fakeLoginRepo.GetByUserNameAsync<LoginModel>(userName))
                .Returns(Task.FromResult<LoginModel?>(null));

            // Act
            var result = await _sut.GetUserLoginDataByUserNameAsync(userName);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserDataByIdAsync_ShouldReturnData_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expectedUser = new UserModel { Id = userId, FullName = "John Doe" };

            A.CallTo(() => _fakeUsernRepo.GetByIdAsync(userId))
                .Returns(Task.FromResult(expectedUser!));

            // Act
            var result = await _sut.GetUserDataByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Id, result.Id);
            Assert.Equal(expectedUser.FullName, result.FullName);
        }

        [Fact]
        public async Task GetUserDataByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();

            A.CallTo(() => _fakeUsernRepo.GetByIdAsync(userId))
                .Returns(Task.FromResult<UserModel?>(null));

            // Act
            var result = await _sut.GetUserDataByIdAsync(userId);

            // Assert
            Assert.Null(result);
        }

    }
}
