using FakeItEasy;
using MockQueryable;
using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;
using MyTasks.Repositories.DTOS;
using MyTasks.Repositories.Repositories.UserDataRepository;

namespace MyTasks.Tests
{
    public class UserDataRepositoryTests
    {
        private readonly IUserOperationsRepository _fakeRepository;
        private readonly UserDataRepository _sut;
        public UserDataRepositoryTests()
        {
            _fakeRepository = A.Fake<IUserOperationsRepository>();
            _sut = new UserDataRepository(_fakeRepository);
        }

        [Fact]
        public async Task AddUserDataAsync_ShouldReturnUserResponseDto_WhenUserIsAdded()
        {
            //Arrange
            var fakeData = new UserWithLoginDto
            {
                FullName = "John Doe",
                Username = "johndoe",
                PasswordHash = "hashedPassword123",
                Type = UserType.Regular
            };

            //Act
            var fakeUserResponseDto = await _sut.AddUserDataAsync(fakeData);

            //Assert
            Assert.NotNull(fakeUserResponseDto);
            Assert.Equal(fakeData.FullName,fakeUserResponseDto.FullName);
            Assert.Equal(fakeData.Username, fakeUserResponseDto.Username);
            Assert.Equal(fakeData.Type, fakeUserResponseDto.Type);
            Assert.False(fakeUserResponseDto.IsDeleted);
        }

        [Fact]
        public async Task UpdateUserDataAsync_ShouldUpdateUserData_WhenNevDataIsProvided()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var fakeData = new UserWithLoginDto
            {
                FullName = "John Doe2",
                Username = "johndoe2",
                PasswordHash = "hashedPassword123",
                Type = UserType.Admin
            };
            var fakeUser = CreateFakeUser(userId);

            A.CallTo(() => _fakeRepository.GetUserAndLoginDataByIDAsync(userId))
                .ReturnsLazily(() => Task.FromResult<UserModel?>(fakeUser));

            A.CallTo(() => _fakeRepository.UpdateUserDataAsync(A<UserModel>.Ignored))
                .Invokes((UserModel updatedUser) =>
                {
                    fakeUser.FullName = updatedUser.FullName;
                    fakeUser.LoginModel.Username = updatedUser.LoginModel!.Username;
                    fakeUser.LoginModel.Type = updatedUser.LoginModel.Type;
                    fakeUser.LoginModel.PasswordHash = updatedUser.LoginModel.PasswordHash;
                });

            //Act
            await _sut.UpdateUserDataAsync(userId, fakeData);

            //Assert
            Assert.Equal(fakeUser.FullName, fakeData.FullName);
            Assert.Equal(fakeUser.LoginModel.Username, fakeData.Username);
            Assert.Equal(fakeUser.LoginModel.Type, fakeData.Type);
            Assert.NotEmpty(fakeUser.LoginModel.PasswordHash);
        }

        [Fact]
        public async Task UpdateUserDataAsync_ShouldThrow_WhenUserNotFound()
        {
            //Arrange
            var userId = Guid.NewGuid();

            A.CallTo(() => _fakeRepository.GetUserAndLoginDataByIDAsync(userId))
                .ReturnsLazily(() => Task.FromResult<UserModel?>(null));

            //Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(async () => 
            await _sut.UpdateUserDataAsync(userId, new UserWithLoginDto()));

            //Assert
            Assert.Equal($"User with ID {userId} not found.", exception.Message);
        }

        [Fact]
        public async Task HardDeleteUserDataAsync_ShouldDeleteUser()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var fakeUser = CreateFakeUser(userId);

            A.CallTo(() => _fakeRepository.GetUserAndLoginDataByIDAsync(userId))
                .ReturnsLazily(() => Task.FromResult<UserModel?>(fakeUser));

            //Act
            await _sut.HardDeleteUserDataAsync(userId);

            //Assert
            A.CallTo(() => _fakeRepository.DeleteUserDataAsync(fakeUser))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task HardDeleteUserDataAsync_ShouldThrow_WhenUserNotFound()
        {
            //Arrange
            var userId = Guid.NewGuid();
           
            A.CallTo(() => _fakeRepository.GetUserAndLoginDataByIDAsync(userId))
                .ReturnsLazily(() => Task.FromResult<UserModel?>(null));

            //Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            await _sut.HardDeleteUserDataAsync(userId));

            //Assert
            Assert.Equal($"User with ID {userId} not found.", exception.Message);
        }

        [Fact]
        public async Task HardDeleteUserDataAsync_ShouldThrow_WhenTypeAdmin()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var fakeUser = CreateFakeUser(userId);
            fakeUser.LoginModel.Type = UserType.Admin;

            A.CallTo(() => _fakeRepository.GetUserAndLoginDataByIDAsync(userId))
                .ReturnsLazily(() => Task.FromResult<UserModel?>(fakeUser));

            //Act
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _sut.HardDeleteUserDataAsync(userId));

            //Assert
            Assert.Equal("Cannot delete an Admin user.", exception.Message);
        }

        [Fact]
        public async Task SoftDeleteUserDataAsync_ShouldThrow_WhenUserNotFound()
        {
            //Arrange
            var userId = Guid.NewGuid();

            A.CallTo(() => _fakeRepository.GetUserAndLoginDataByIDAsync(userId))
                .ReturnsLazily(() => Task.FromResult<UserModel?>(null));

            //Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            await _sut.SoftDeleteUserDataAsync(userId));

            //Assert
            Assert.Equal($"User with ID {userId} not found.", exception.Message);
        }

        [Fact]
        public async Task SoftDeleteUserDataAsync_ShouldThrow_WhenTypeAdmin()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var fakeUser = CreateFakeUser(userId);
            fakeUser.LoginModel.Type = UserType.Admin;

            A.CallTo(() => _fakeRepository.GetUserAndLoginDataByIDAsync(userId))
                .ReturnsLazily(() => Task.FromResult<UserModel?>(fakeUser));

            //Act
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _sut.SoftDeleteUserDataAsync(userId));

            //Assert
            Assert.Equal("Cannot delete an Admin user.", exception.Message);
        }

        [Fact]
        public async Task SoftDeleteUserDataAsync_ShouldDeleteUser()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var fakeUser = CreateFakeUser(userId);

            A.CallTo(() => _fakeRepository.GetUserAndLoginDataByIDAsync(userId))
                .ReturnsLazily(() => Task.FromResult<UserModel?>(fakeUser));

            A.CallTo(() => _fakeRepository.UpdateUserDataAsync(A<UserModel?>.Ignored))
                .Invokes((UserModel updatedUser) =>
                {
                    updatedUser.IsDeleted = true;
                });

            //Act
            await _sut.SoftDeleteUserDataAsync(userId);

            //Assert
            Assert.True(fakeUser.IsDeleted);
        }

        [Fact]
        public async Task GetAllUserDataAsync_ShouldThrow_WhenNoUsersAreFound()
        {
            //Arrange
            var fakeData = new List<UserModel>();
            var fakeDataQueryable = fakeData.BuildMock();

            A.CallTo(() => _fakeRepository.GetAllUserAndLoginData())
            .Returns(fakeDataQueryable);

            //Act
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _sut.GetAllUserDataAsync());

            //Assert
            Assert.Equal("No data found", exception.Message);
        }

        [Fact]
        public async Task GetAllUserDataAsync_ShouldReturn_UserResponseDto()
        {
            //Arrange
            var fakeData = CreateFakeUsers();
            var fakeDataQueryable = fakeData.BuildMock();

            A.CallTo(() => _fakeRepository.GetAllUserAndLoginData())
            .Returns(fakeDataQueryable);

            //Act
            var result = await _sut.GetAllUserDataAsync();

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(fakeData.Count, result.Count);
        }

        [Fact]
        public async Task GetUserDataAsync_ShouldReturn_UserResponseDto()
        {
            //Arrange
            var id = Guid.NewGuid();
            var fakeUser = CreateFakeUser(id);

            A.CallTo(() => _fakeRepository.GetUserByIdAsync(id))
                .ReturnsLazily(() => Task.FromResult<UserModel?>(fakeUser));

            //Act
            var userdata = await _sut.GetUserDataAsync(id);

            //Assert
            Assert.NotNull(userdata);
            Assert.Equal(fakeUser.Id, userdata!.Id);
            Assert.Equal(fakeUser.FullName, userdata.FullName);
            Assert.Equal(fakeUser.LoginModel!.Username, userdata.Username);
            Assert.Equal(fakeUser.LoginModel.Type, userdata.Type);
            Assert.Equal(fakeUser.IsDeleted, userdata.IsDeleted);
        }

        [Fact]
        public async Task GetUserDataAsync_ShouldReturnNull_WhenNoUserFound()
        {
            //Arrange
            var id = Guid.NewGuid();

            A.CallTo(() => _fakeRepository.GetUserByIdAsync(id))
                .ReturnsLazily(() => Task.FromResult<UserModel?>(null));

            //Act
            var userdata = await _sut.GetUserDataAsync(id);

            //Assert
            Assert.Null(userdata);
        }

        private UserModel CreateFakeUser(Guid userId)
        {
            return new UserModel
            {
                Id = userId,
                FullName = "John Doe",
                LoginId = Guid.NewGuid(),
                LoginModel = new LoginModel
                {
                    Id = Guid.NewGuid(),
                    Username = "johndoe",
                    Type = UserType.Regular,
                    UserId = Guid.NewGuid(),
                    PasswordHash = "Test"
                },
                IsDeleted = false
            };
        }

        private ICollection<UserModel> CreateFakeUsers()
        {
           return new List<UserModel>
            {
                new UserModel
                {
                    Id = Guid.NewGuid(),
                    FullName = "John Doe",
                    LoginId = Guid.NewGuid(),
                    LoginModel = new LoginModel
                    {
                        Id = Guid.NewGuid(),
                        Username = "johndoe",
                        Type = UserType.Regular,
                        UserId = Guid.NewGuid()
                    },
                    IsDeleted = false
                },
                new UserModel
                {
                    Id = Guid.NewGuid(),
                    FullName = "Jane Smith",
                    LoginId = Guid.NewGuid(),
                    LoginModel = new LoginModel
                    {
                        Id = Guid.NewGuid(),
                        Username = "janesmith",
                        Type = UserType.Admin,
                        UserId = Guid.NewGuid()
                    },
                    IsDeleted = false
                },
                new UserModel
                {
                    Id = Guid.NewGuid(),
                    FullName = "Deleted User",
                    LoginId = Guid.NewGuid(),
                    LoginModel = null, // brak loginu
                    IsDeleted = true
                }
            };
        }
    }
}
