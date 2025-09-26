using FakeItEasy;
using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;
using MyTasks.Repositories.Repositories.ProjecRepository;

namespace MyTasks.Tests
{
    public class ProjecRepositoryTests
    {
        private readonly IDbRepository<ProjectModel> _fakeProjectRepository;
        private readonly IProjectOperationsRepository _fakeProjectOperationsRepository;
        private readonly ProjecRepository _sut;

        public ProjecRepositoryTests()
        {
            _fakeProjectRepository = A.Fake<IDbRepository<ProjectModel>>();
            _fakeProjectOperationsRepository = A.Fake<IProjectOperationsRepository>();
            _sut = new ProjecRepository(_fakeProjectRepository, _fakeProjectOperationsRepository);
        }

        [Fact]
        public async Task AddProject_ShouldCall_AddEntityAsync()
        {
            // Arrange
            var project = new ProjectModel
            {
                Id = Guid.NewGuid(),
                Name = "Test Project",
                Description = "Some description"
            };

            // Act
            await _sut.AddProject(project);

            // Assert
            A.CallTo(() => _fakeProjectRepository.AddEntityAsync(project))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateProject_ShouldCall_UpdateEntityAsync()
        {
            // Arrange
            var project = new ProjectModel
            {
                Id = Guid.NewGuid(),
                Name = "Old Name"
            };

            // Act
            await _sut.UpdateProject(project);

            // Assert
            A.CallTo(() => _fakeProjectRepository.UpdateEntityAsync(project))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturn_Project_WhenFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var project = new ProjectModel
            {
                Id = id,
                Name = "Found Project"
            };

            A.CallTo(() => _fakeProjectRepository.GetByIdAsync(id))
                .Returns(Task.FromResult<ProjectModel?>(project));

            // Act
            var result = await _sut.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result!.Id);
            Assert.Equal("Found Project", result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturn_Null_WhenNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            A.CallTo(() => _fakeProjectRepository.GetByIdAsync(id))
                .Returns(Task.FromResult<ProjectModel?>(null));

            // Act
            var result = await _sut.GetByIdAsync(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteProjectByIdAsync_ShouldCall_DeleteProjectWithTasksAndCommentsByIdAsync()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            await _sut.DeleteProjectByIdAsync(id);

            // Assert
            A.CallTo(() => _fakeProjectOperationsRepository.DeleteProjectWithTasksAndCommentsByIdAsync(id))
                .MustHaveHappenedOnceExactly();
        }
    }
}