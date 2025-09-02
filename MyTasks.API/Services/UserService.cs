using MyTasks.API.Services.Interfaces;
using MyTasks.Repositories.DTOS;
using MyTasks.Repositories.Interfaces.IUserDataRepository;

namespace MyTasks.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDataRepository _repository;

        public UserService(IUserDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            return await _repository.GetAllUserData();
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(Guid userId)
        {
            return await _repository.GetUserData(userId);
        }

        public async Task<UserResponseDto> CreateUserAsync(UserWithLoginDto data)
        {
            return await _repository.AddUserData(data);
        }

        public async Task UpdateUserAsync(Guid userId, UserWithLoginDto data)
        {
            await _repository.UpdateUserData(userId, data);
        }

        public async Task DeactivateUserAsync(Guid userId)
        {
            await _repository.SoftDeleteUserData(userId);
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            await _repository.HardDeleteUserData(userId);
        }
    }
}
