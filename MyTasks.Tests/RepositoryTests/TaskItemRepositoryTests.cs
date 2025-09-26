using FakeItEasy;
using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;
using MyTasks.Repositories.Repositories.TaskItemRepository;

namespace MyTasks.Tests.RepositoryTests
{
    public class TaskItemRepositoryTests
    {
        private readonly IDbRepository<TaskItemModel> _fakeTaskItemRepository;
        private readonly ITaskItemOperationsRepository _fakeTaskItemOperationsRepository;
        private readonly TaskItemRepository _sut;

        public TaskItemRepositoryTests()
        {
            _fakeTaskItemRepository = A.Fake<IDbRepository<TaskItemModel>>();
            _fakeTaskItemOperationsRepository = A.Fake<ITaskItemOperationsRepository>();
            _sut = new TaskItemRepository(_fakeTaskItemRepository, _fakeTaskItemOperationsRepository);
        }

        [Fact]
        public async Task AddTaskItemAsync_ShouldCall_AddEntityAsync()
        {
            // Arrange
            var taskItem = new TaskItemModel
            {
                Id = Guid.NewGuid(),
                Title = "New Task"
            };

            // Act
            await _sut.AddTaskItemAsync(taskItem);

            // Assert
            A.CallTo(() => _fakeTaskItemRepository.AddEntityAsync(taskItem))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateTaskItemAsync_ShouldCall_UpdateEntityAsync()
        {
            // Arrange
            var taskItem = new TaskItemModel
            {
                Id = Guid.NewGuid(),
                Title = "Existing Task"
            };

            // Act
            await _sut.UpdateTaskItemAsync(taskItem);

            // Assert
            A.CallTo(() => _fakeTaskItemRepository.UpdateEntityAsync(taskItem))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetTaskItemByIdAsync_ShouldReturn_TaskItem()
        {
            // Arrange
            var id = Guid.NewGuid();
            var taskItem = new TaskItemModel
            {
                Id = id,
                Title = "Test Task"
            };

            A.CallTo(() => _fakeTaskItemRepository.GetByIdAsync(id))
                .Returns(Task.FromResult<TaskItemModel?>(taskItem));

            // Act
            var result = await _sut.GetTaskItemtByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result!.Id);
            Assert.Equal("Test Task", result.Title);
        }

        [Fact]
        public async Task DeleteTaskItemByIdAsync_ShouldCall_DeleteTaskItemtWithCommentsByIdAsync()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            await _sut.DeleteTaskItemtByIdAsync(id);

            // Assert
            A.CallTo(() => _fakeTaskItemOperationsRepository.DeleteTaskItemtWithCommentsByIdAsync(id))
                .MustHaveHappenedOnceExactly();
        }
    }
}