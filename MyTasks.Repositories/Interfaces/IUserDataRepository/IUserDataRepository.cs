using MyTasks.Repositories.DTOS;

namespace MyTasks.Repositories.Interfaces.IUserDataRepository
{
    public interface IUserDataRepository
    {
        Task AddUserData(UserWithLoginDto data);
        Task UpdateUserData(Guid? userId, UserWithLoginDto data);
        Task HardDeleteUserData(Guid? userId);
        Task SoftDeleteUserData(Guid? userId);
    }
}