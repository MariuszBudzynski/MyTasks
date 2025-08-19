using FakeItEasy;
using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;
using MyTasks.Repositories.Repositories.LoginRepository;

namespace MyTasks.Tests
{
    //for tests use FakeItEasy
    public class LoginRepositoryTests
    {
        [Fact]
        public async Task GetUserLoginDataById_ShouldReturnData_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expectedUser = new LoginModel { Id = userId, Username = "testuser" };
            var fakeRepo = A.Fake<IDbRepository<LoginModel>>();

            A.CallTo(() => fakeRepo.GetById(userId))
            .Returns(Task.FromResult(expectedUser!));

            var sut = new LoginRepository(fakeRepo);

            // Act
            var result = await sut.GetUserLoginDataById(userId);

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
            var fakeRepo = A.Fake<IDbRepository<LoginModel>>();

            A.CallTo(() => fakeRepo.GetById(userId))
            .Returns(Task.FromResult<LoginModel?>(null));

            var sut = new LoginRepository(fakeRepo);

            // Act
            var result = await sut.GetUserLoginDataById(userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserLoginDataByUserName_ShouldReturnData_WhenUserExists()
        {
            // Arrange
            var expectedUser = new LoginModel {Username = "testuser" };
            var fakeRepo = A.Fake<IDbRepository<LoginModel>>();

            A.CallTo(() => fakeRepo.GetByUserName<LoginModel>(expectedUser.Username))
            .Returns(Task.FromResult(expectedUser!));

            var sut = new LoginRepository(fakeRepo);

            // Act
            var result = await sut.GetUserLoginDataByUserName(expectedUser.Username);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Username, result.Username);
        }

        [Fact]
        public async Task GetUserLoginDataByUserName_ShouldReturnNull_WhenUserDoesNotExists()
        {
            // Arrange
            var userName = "testUserName";
            var fakeRepo = A.Fake<IDbRepository<LoginModel>>();

            A.CallTo(() => fakeRepo.GetByUserName<LoginModel>(userName))
                .Returns(Task.FromResult<LoginModel?>(null));

            var sut = new LoginRepository(fakeRepo);

            // Act
            var result = await sut.GetUserLoginDataByUserName(userName);

            // Assert
            Assert.Null(result);
        }
    }
}
