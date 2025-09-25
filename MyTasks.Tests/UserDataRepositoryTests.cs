using FakeItEasy;
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
    }
}
