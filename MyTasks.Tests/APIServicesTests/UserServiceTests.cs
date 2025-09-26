using FakeItEasy;
using MyTasks.API.Services;
using MyTasks.Repositories.DTOS;
using MyTasks.Repositories.Interfaces.IUserDataRepository;

namespace MyTasks.Tests
{
    public class UserServiceTests
    {
        private readonly IUserDataRepository _fakeRepository;
        private readonly UserService _sut;

        public UserServiceTests()
        {
            _fakeRepository = A.Fake<IUserDataRepository>();
            _sut = new UserService(_fakeRepository);
        }

        [Fact]
        public async Task GetAllUsersAsync_ShouldReturnUsers()
        {
            // Arrange
            var fakeUsers = new List<UserResponseDto>
            {
                new UserResponseDto(Guid.NewGuid(), "John Doe", "johndoe", MyTasks.Models.Models.UserType.Admin, false),
                new UserResponseDto(Guid.NewGuid(), "Jane Smith", "janesmith", MyTasks.Models.Models.UserType.Regular, false)
            };

            A.CallTo(() => _fakeRepository.GetAllUserDataAsync())
             .ReturnsLazily(c => Task.FromResult<ICollection<UserResponseDto>>(fakeUsers));

            // Act
            var result = await _sut.GetAllUsersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var fakeUser = new UserResponseDto(userId, "John Doe", "johndoe", MyTasks.Models.Models.UserType.Admin, false);
            A.CallTo(() => _fakeRepository.GetUserDataAsync(userId))
                .Returns(Task.FromResult<UserResponseDto?>(fakeUser));

            // Act
            var result = await _sut.GetUserByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(fakeUser.Id, result!.Id);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            A.CallTo(() => _fakeRepository.GetUserDataAsync(userId))
                .Returns(Task.FromResult<UserResponseDto?>(null));

            // Act
            var result = await _sut.GetUserByIdAsync(userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateUserAsync_ShouldReturnCreatedUser()
        {
            // Arrange
            var newUser = new UserWithLoginDto
            {
                FullName = "John Doe",
                Username = "johndoe",
                PasswordHash = "hashedpassword",
                Type = MyTasks.Models.Models.UserType.Admin
            };
            var createdUser = new UserResponseDto(Guid.NewGuid(), newUser.FullName, newUser.Username, newUser.Type, false);
            A.CallTo(() => _fakeRepository.AddUserDataAsync(newUser))
                .Returns(Task.FromResult(createdUser));

            // Act
            var result = await _sut.CreateUserAsync(newUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newUser.FullName, result.FullName);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldCallRepository()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var updatedUser = new UserWithLoginDto
            {
                FullName = "Updated Name",
                Username = "updatedusername",
                PasswordHash = "newpass",
                Type = MyTasks.Models.Models.UserType.Regular
            };

            // Act
            await _sut.UpdateUserAsync(userId, updatedUser);

            // Assert
            A.CallTo(() => _fakeRepository.UpdateUserDataAsync(userId, updatedUser))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task DeactivateUserAsync_ShouldCallSoftDelete()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act
            await _sut.DeactivateUserAsync(userId);

            // Assert
            A.CallTo(() => _fakeRepository.SoftDeleteUserDataAsync(userId))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldCallHardDelete()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act
            await _sut.DeleteUserAsync(userId);

            // Assert
            A.CallTo(() => _fakeRepository.HardDeleteUserDataAsync(userId))
                .MustHaveHappenedOnceExactly();
        }
    }
}