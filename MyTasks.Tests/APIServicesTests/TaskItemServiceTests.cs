using FakeItEasy;
using MyTasks.Models.Models;
using MyTasks.Repositories.DTOS;
using MyTasks.Repositories.Interfaces.ITaskItemRepository;
using MyTasks.Services;

namespace MyTasks.Tests
{
    public class TaskItemServiceTests
    {
        private readonly ITaskItemRepository _fakeTaskItemRepo;
        private readonly TaskItemService _sut;

        public TaskItemServiceTests()
        {
            _fakeTaskItemRepo = A.Fake<ITaskItemRepository>();
            _sut = new TaskItemService(_fakeTaskItemRepo);
        }

        [Fact]
        public async Task GetTaskItemById_ShouldReturnTaskItem_WhenExists()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var fakeTask = new TaskItemModel { Id = taskId, Title = "Test Task" };

            A.CallTo(() => _fakeTaskItemRepo.GetTaskItemtByIdAsync(taskId))
                .Returns(Task.FromResult<TaskItemModel?>(fakeTask));

            // Act
            var result = await _sut.GetTaskItemById(taskId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(taskId, result!.Id);
            Assert.Equal(fakeTask.Title, result.Title);
        }

        [Fact]
        public async Task GetTaskItemById_ShouldReturnNull_WhenNotExists()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            A.CallTo(() => _fakeTaskItemRepo.GetTaskItemtByIdAsync(taskId))
                .Returns(Task.FromResult<TaskItemModel?>(null));

            // Act
            var result = await _sut.GetTaskItemById(taskId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldCallRepositoryAndReturnResponseDto()
        {
            // Arrange
            var ownerId = Guid.NewGuid();
            var dto = new CreateTaskItemDto("New Task", "Description", DateTime.UtcNow, false, null);

            // Act
            var result = await _sut.CreateAsync(dto, ownerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Title, result.Title);
            Assert.Equal(dto.Description, result.Description);
            Assert.Equal(dto.DueDate, result.DueDate);
            Assert.Equal(dto.IsCompleted, result.IsCompleted);
            Assert.Equal(ownerId, result.AssignedUserId);

            A.CallTo(() => _fakeTaskItemRepo.AddTaskItemAsync(
                A<TaskItemModel>.That.Matches(
                    t => t.Title == dto.Title &&
                         t.Description == dto.Description &&
                         t.DueDate == dto.DueDate &&
                         t.IsCompleted == dto.IsCompleted &&
                         t.AssignedUserId == ownerId
                ))).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateTaskItemtAsync_ShouldCallRepository()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var existingTask = new TaskItemModel { Id = taskId, Title = "Old Title" };
            var updateDto = new UpdateTaskItemDto("Updated Title", "Updated Desc", DateTime.UtcNow, true);

            // Act
            await _sut.UpdateTaskItemtAsync(updateDto, existingTask, taskId);

            // Assert
            Assert.Equal(updateDto.Title, existingTask.Title);
            Assert.Equal(updateDto.Description, existingTask.Description);
            Assert.Equal(updateDto.IsCompleted, existingTask.IsCompleted);
            Assert.Equal(updateDto.DueDate ?? DateTime.MinValue, existingTask.DueDate);

            A.CallTo(() => _fakeTaskItemRepo.UpdateTaskItemAsync(existingTask))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnTaskItemResponseDto_WhenExists()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var task = new TaskItemModel
            {
                Id = taskId,
                Title = "Test Task",
                Description = "Desc",
                DueDate = DateTime.UtcNow,
                IsCompleted = true,
                ProjectId = Guid.NewGuid(),
                AssignedUserId = Guid.NewGuid()
            };

            A.CallTo(() => _fakeTaskItemRepo.GetTaskItemtByIdAsync(taskId))
                .Returns(Task.FromResult<TaskItemModel?>(task));

            // Act
            var result = await _sut.GetByIdAsync(taskId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(task.Id, result!.Id);
            Assert.Equal(task.Title, result.Title);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            A.CallTo(() => _fakeTaskItemRepo.GetTaskItemtByIdAsync(taskId))
                .Returns(Task.FromResult<TaskItemModel?>(null));

            // Act
            var result = await _sut.GetByIdAsync(taskId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteTaskItemtWithDataByIdAsync_ShouldCallRepository()
        {
            // Arrange
            var taskId = Guid.NewGuid();

            // Act
            await _sut.DeleteTaskItemtWithDataByIdAsync(taskId);

            // Assert
            A.CallTo(() => _fakeTaskItemRepo.DeleteTaskItemtByIdAsync(taskId))
                .MustHaveHappenedOnceExactly();
        }
    }
}