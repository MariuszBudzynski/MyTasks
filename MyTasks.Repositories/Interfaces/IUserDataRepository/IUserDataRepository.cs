using MyTasks.Repositories.DTOS;

namespace MyTasks.Repositories.Interfaces.IUserDataRepository
{
    public interface IUserDataRepository
    {
        Task<UserResponseDto> AddUserData(UserWithLoginDto data);
        Task UpdateUserData(Guid? userId, UserWithLoginDto data);
        Task HardDeleteUserData(Guid? userId);
        Task SoftDeleteUserData(Guid? userId);
        Task<ICollection<UserResponseDto>> GetAllUserData();
        Task<UserResponseDto?> GetUserData(Guid id);
    }
}