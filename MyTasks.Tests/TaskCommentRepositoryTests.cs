using FakeItEasy;
using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;
using MyTasks.Repositories.Repositories.TaskCommentRepository;

namespace MyTasks.Tests
{
    public class TaskCommentRepositoryTests
    {
        private readonly IDbRepository<TaskCommentModel> _fakeTaskCommentRepository;
        private readonly TaskCommentRepository _sut;

        public TaskCommentRepositoryTests()
        {
            _fakeTaskCommentRepository = A.Fake<IDbRepository<TaskCommentModel>>();
            _sut = new TaskCommentRepository(_fakeTaskCommentRepository);
        }

        [Fact]
        public async Task AddTaskComment_ShouldCall_AddEntityAsync()
        {
            // Arrange
            var comment = new TaskCommentModel
            {
                Id = Guid.NewGuid(),
                Content = "Test comment",
                CreatedAt = DateTime.UtcNow
            };

            // Act
            await _sut.AddTaskComment(comment);

            // Assert
            A.CallTo(() => _fakeTaskCommentRepository.AddEntityAsync(comment))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturn_TaskComment_WhenFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var comment = new TaskCommentModel
            {
                Id = id,
                Content = "Another test comment",
                CreatedAt = DateTime.UtcNow
            };

            A.CallTo(() => _fakeTaskCommentRepository.GetByIdAsync(id))
                .Returns(Task.FromResult<TaskCommentModel?>(comment));

            // Act
            var result = await _sut.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result!.Id);
            Assert.Equal("Another test comment", result.Content);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturn_Null_WhenNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            A.CallTo(() => _fakeTaskCommentRepository.GetByIdAsync(id))
                .Returns(Task.FromResult<TaskCommentModel?>(null));

            // Act
            var result = await _sut.GetByIdAsync(id);

            // Assert
            Assert.Null(result);
        }
    }
}