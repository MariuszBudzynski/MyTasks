using FakeItEasy;
using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;
using MyTasks.Repositories.Repositories.LoginRepository;

namespace MyTasks.Tests
{
    //for tests use FakeItEasy
    public class LoginRepositoryTests
    {
        private readonly IDbRepository<LoginModel> _fakeRepo;
        private readonly LoginRepository _sut;

        public LoginRepositoryTests()
        {
            _fakeRepo = A.Fake<IDbRepository<LoginModel>>();
            //_sut = new LoginRepository(_fakeRepo); //fix this later on
        }

        [Fact]
        public async Task GetUserLoginDataById_ShouldReturnData_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expectedUser = new LoginModel { Id = userId, Username = "testuser" };

            A.CallTo(() => _fakeRepo.GetById(userId))
            .Returns(Task.FromResult(expectedUser!));

            // Act
            var result = await _sut.GetUserLoginDataById(userId);

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

            A.CallTo(() => _fakeRepo.GetById(userId))
            .Returns(Task.FromResult<LoginModel?>(null));

            // Act
            var result = await _sut.GetUserLoginDataById(userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserLoginDataByUserName_ShouldReturnData_WhenUserExists()
        {
            // Arrange
            var expectedUser = new LoginModel {Username = "testuser" };

            A.CallTo(() => _fakeRepo.GetByUserName<LoginModel>(expectedUser.Username))
            .Returns(Task.FromResult(expectedUser!));

            // Act
            var result = await _sut.GetUserLoginDataByUserName(expectedUser.Username);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Username, result.Username);
        }

        [Fact]
        public async Task GetUserLoginDataByUserName_ShouldReturnNull_WhenUserDoesNotExists()
        {
            // Arrange
            var userName = "testUserName";

            A.CallTo(() => _fakeRepo.GetByUserName<LoginModel>(userName))
                .Returns(Task.FromResult<LoginModel?>(null));

            // Act
            var result = await _sut.GetUserLoginDataByUserName(userName);

            // Assert
            Assert.Null(result);
        }
    }
}
