using MyTasks.Repositories.DTOS;

namespace MyTasks.Repositories.Interfaces.IUserDataRepository
{
    public interface IUserDataRepository
    {
        Task<UserResponseDto> AddUserDataAsync(UserWithLoginDto data);
        Task UpdateUserDataAsync(Guid? userId, UserWithLoginDto data);
        Task HardDeleteUserDataAsync(Guid? userId);
        Task SoftDeleteUserDataAsync(Guid? userId);
        Task<ICollection<UserResponseDto>> GetAllUserDataAsync();
        Task<UserResponseDto?> GetUserDataAsync(Guid id);
    }
}