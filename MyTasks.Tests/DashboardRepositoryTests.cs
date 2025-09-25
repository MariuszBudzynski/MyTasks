using FakeItEasy;
using MockQueryable;
using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;
using MyTasks.Repositories.Repositories.DashboardRepository;

namespace MyTasks.Tests
{
    public class DashboardRepositoryTests
    {
        private readonly IProjectOperationsRepository _fakeProjectRepo;
        private readonly DashboardRepository _sut;
        public DashboardRepositoryTests()
        {
            _fakeProjectRepo = A.Fake<IProjectOperationsRepository>();
            _sut = new DashboardRepository(_fakeProjectRepo);
        }

        [Fact]
        public async Task GetProjectsData_ShouldThrow_WhenUserNameIsEmpty()
        {
            //Arrange
            var userName = String.Empty;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _sut.GetProjectsDataAsync(userName));
        }

        [Fact]
        public async Task GetProjectsData_ShouldReturnDashboardDto_WhenProjectsExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userLoginId = Guid.NewGuid();
            var userName = "testuser";

            var fakeProjects = CreateFakeProjects(userId, userLoginId, userName);

            var fakeProjectsAsQueryable = fakeProjects.BuildMock();

            A.CallTo(() => _fakeProjectRepo.GetProjectsWithTasksAndCommentsAsync(userName))
                .ReturnsLazily(() => Task.FromResult(fakeProjectsAsQueryable));

            // Act
            var result = await _sut.GetProjectsDataAsync(userName);

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

        private List<ProjectModel> CreateFakeProjects(Guid userId, Guid userLoginId, string userName)
        {
            return new List<ProjectModel>
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
        }
    }
}
