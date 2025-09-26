using FakeItEasy;
using MyTasks.API.Services;
using MyTasks.Models.Models;
using MyTasks.Repositories.DTOS;
using MyTasks.Repositories.Interfaces.ITaskCommentRepository;

namespace MyTasks.Tests
{
    public class TaskCommentServiceTests
    {
        private readonly ITaskCommentRepository _fakeTaskCommentRepo;
        private readonly TaskCommentService _sut;

        public TaskCommentServiceTests()
        {
            _fakeTaskCommentRepo = A.Fake<ITaskCommentRepository>();
            _sut = new TaskCommentService(_fakeTaskCommentRepo);
        }

        [Fact]
        public async Task GetTaskCommentByIdAsync_ShouldReturnComment_WhenExists()
        {
            // Arrange
            var commentId = Guid.NewGuid();
            var fakeComment = new TaskCommentModel
            {
                Id = commentId,
                Content = "Test Comment",
                CreatedAt = DateTime.UtcNow
            };

            A.CallTo(() => _fakeTaskCommentRepo.GetByIdAsync(commentId))
                .Returns(Task.FromResult<TaskCommentModel?>(fakeComment));

            // Act
            var result = await _sut.GetTaskCommentByIdAsync(commentId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(commentId, result!.Id);
            Assert.Equal(fakeComment.Content, result.Content);
            Assert.Equal(fakeComment.CreatedAt, result.CreatedAt);
        }

        [Fact]
        public async Task GetTaskCommentByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            // Arrange
            var commentId = Guid.NewGuid();

            A.CallTo(() => _fakeTaskCommentRepo.GetByIdAsync(commentId))
                .Returns(Task.FromResult<TaskCommentModel?>(null));

            // Act
            var result = await _sut.GetTaskCommentByIdAsync(commentId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateTaskCommentAsync_ShouldCallRepositoryAndReturnResponseDto()
        {
            // Arrange
            var ownerId = Guid.NewGuid();
            var dto = new CreateTaskCommentDto("New Comment", DateTime.UtcNow, Guid.NewGuid());

            // Act
            var result = await _sut.CreateTaskCommentAsync(dto, ownerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Content, result.Content);
            Assert.Equal(dto.CreatedAt, result.CreatedAt);

            A.CallTo(() => _fakeTaskCommentRepo.AddTaskComment(A<TaskCommentModel>.That.Matches(
                c => c.Content == dto.Content &&
                     c.CreatedAt == dto.CreatedAt &&
                     c.TaskItemId == dto.TaskItemId &&
                     c.AuthorId == ownerId
            ))).MustHaveHappenedOnceExactly();
        }
    }
}
