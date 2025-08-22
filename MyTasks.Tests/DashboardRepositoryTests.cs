using FakeItEasy;
using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;
using MyTasks.Repositories.Repositories.DashboardRepository;

namespace MyTasks.Tests
{
    public class DashboardRepositoryTests
    {
        private readonly IProjectRepository _fakeProjectRepo;
        private readonly DashboardRepository _sut;
        public DashboardRepositoryTests()
        {
            _fakeProjectRepo = A.Fake<IProjectRepository>();
            _sut = new DashboardRepository(_fakeProjectRepo);
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

        [Fact]
        public async Task GetProjectsData_ShouldReturnDashboardDto_WhenProjectsExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userLoginId = Guid.NewGuid();
            var userName = "testuser";

            var fakeProjects = new List<ProjectModel>
            {
                new ProjectModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Project",
                    Description = "Sample project",
                    OwnerId = userId,
                    Owner = new UserModel
                    {
                        Id = userId,
                        FullName = "John Doe",
                        LoginId = userLoginId,
                        LoginModel = new LoginModel
                        {
                            Id = userLoginId,
                            Username = userName,
                            Type = UserType.Regular,
                            UserId = userId
                        }
                    },
                    Tasks = new List<TaskItemModel>
                    {
                        new TaskItemModel
                        {
                            Id = Guid.NewGuid(),
                            Title = "Task 1",
                            Description = "Do something",
                            IsCompleted = true,
                            Comments = new List<TaskCommentModel>
                            {
                                new TaskCommentModel
                                {
                                    Id = Guid.NewGuid(),
                                    Content = "Comment 1",
                                    CreatedAt = DateTime.UtcNow
                                }
                            }
                        },
                        new TaskItemModel
                        {
                            Id = Guid.NewGuid(),
                            Title = "Task 2",
                            Description = "Do something else",
                            IsCompleted = false,
                            Comments = new List<TaskCommentModel>()
                        }
                    }
                }
            };

            A.CallTo(() => _fakeProjectRepo.GetProjectsWithTasksAndComments(userName))
                .Returns(Task.FromResult<ICollection<ProjectModel>>(fakeProjects));

            // Act
            var result = await _sut.GetProjectsData(userName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(userName, result.Username);
            Assert.Equal("John Doe", result.FullName);
            Assert.Equal(UserType.Regular, result.UserType);

            Assert.Single(result.Projects);
            Assert.Equal(2, result.TaskCount);
            Assert.Equal(1, result.CompletedTasks);
            Assert.Equal(1, result.PendingTasks);

            Assert.Contains(result.Tasks, t => t.Title == "Task 1");
            Assert.Contains(result.Tasks, t => t.Title == "Task 2");
            Assert.Equal("Comment 1", result.Tasks.First(t => t.Title == "Task 1").LastComment);
        }
    }
}
