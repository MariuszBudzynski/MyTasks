using MyTasks.Repositories.DTOS;

namespace MyTasks.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> CreateUserAsync(UserWithLoginDto data);
        Task DeactivateUserAsync(Guid userId);
        Task DeleteUserAsync(Guid userId);
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<UserResponseDto?> GetUserByIdAsync(Guid userId);
        Task UpdateUserAsync(Guid userId, UserWithLoginDto data);
    }
}