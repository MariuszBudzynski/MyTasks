using FakeItEasy;
using MyTasks.API.Services;
using MyTasks.Models.Models;
using MyTasks.Repositories.DTOS;
using MyTasks.Repositories.Interfaces.IProjecRepository;

namespace MyTasks.Tests
{
    public class ProjectServiceTests
    {
        private readonly IProjecRepository _fakeProjectRepo;
        private readonly ProjectService _sut;

        public ProjectServiceTests()
        {
            _fakeProjectRepo = A.Fake<IProjecRepository>();
            _sut = new ProjectService(_fakeProjectRepo);
        }

        [Fact]
        public async Task CreateProjectAsync_ShouldCallRepositoryAddProject()
        {
            // Arrange
            var ownerId = Guid.NewGuid();
            var dto = new CreateProjectDto("Test Project", "Sample Description")
            {
                OwnerId = ownerId
            };

            // Act
            var result = await _sut.CreateProjectAsync(dto, ownerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Description, result.Description);
            Assert.Equal(ownerId, result.OwnerId);

            A.CallTo(() => _fakeProjectRepo.AddProject(A<ProjectModel>.That.Matches(
                p => p.Name == dto.Name && p.Description == dto.Description && p.OwnerId == ownerId
            ))).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateProjectAsync_ShouldUpdatePropertiesAndCallRepository()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var existingProject = new ProjectModel
            {
                Id = projectId,
                Name = "Old Name",
                Description = "Old Description"
            };

            var updateDto = new UpdateProjectDto("New Name", "New Description")
            {
                OwnerId = Guid.NewGuid()
            };

            // Act
            await _sut.UpdateProjectAsync(updateDto, existingProject, projectId);

            // Assert
            Assert.Equal(updateDto.Name, existingProject.Name);
            Assert.Equal(updateDto.Description, existingProject.Description);

            A.CallTo(() => _fakeProjectRepo.UpdateProject(existingProject))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetProjectByIdAsync_ShouldReturnProjectFromRepository()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var fakeProject = new ProjectModel { Id = projectId, Name = "Test Project" };

            A.CallTo(() => _fakeProjectRepo.GetByIdAsync(projectId))
                .Returns(Task.FromResult<ProjectModel?>(fakeProject));

            // Act
            var result = await _sut.GetProjectByIdAsync(projectId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(projectId, result!.Id);
            Assert.Equal(fakeProject.Name, result.Name);
        }

        [Fact]
        public async Task DeleteProjectWithDataByIdAsync_ShouldCallRepositoryDelete()
        {
            // Arrange
            var projectId = Guid.NewGuid();

            // Act
            await _sut.DeleteProjectWithDataByIdAsync(projectId);

            // Assert
            A.CallTo(() => _fakeProjectRepo.DeleteProjectByIdAsync(projectId))
                .MustHaveHappenedOnceExactly();
        }
    }
}