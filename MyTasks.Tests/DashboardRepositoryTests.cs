using FakeItEasy;
using MyTasks.DbOperations.Interface;
using MyTasks.Repositories.Repositories.DashboardRepository;

namespace MyTasks.Tests
{
    public class DashboardRepositoryTests
    {
        private readonly DashboardRepository _sut;
        public DashboardRepositoryTests()
        {
            _sut = new DashboardRepository(A.Fake<IProjectRepository>());
        }

        [Fact]
        public async Task GetProjectsData_ShouldThrow_WhenUserNameIsEmpty()
        {
            //Arrange
            var userName = String.Empty;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _sut.GetProjectsData(userName));
        }

        [Fact]
        public async Task GetProjectsData_ShouldThrow_WhenProjectsNotFound()
        {
            //Arrange
            var userName = "RandomNotExistingUserName";

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _sut.GetProjectsData(userName));
        }
    }
}
