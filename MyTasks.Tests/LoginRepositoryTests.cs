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
                .ReturnsLazily(() => Task.FromResult(expectedUser!));

            var sut = new LoginRepository(fakeRepo);

            // Act
            var result = await sut.GetUserLoginDataById(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Id, result.Id);
            Assert.Equal(expectedUser.Username, result.Username);
        }

        [Fact]
        public async Task GetUserLoginDataById_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var fakeRepo = A.Fake<IDbRepository<LoginModel>>();

            A.CallTo(() => fakeRepo.GetById(userId))
                .Returns(Task.FromResult<LoginModel?>(null));

            var sut = new LoginRepository(fakeRepo);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(
                () => sut.GetUserLoginDataById(userId));

            Assert.Equal($"User with Id {userId} not found.", ex.Message);
        }
    }
}
