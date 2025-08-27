using MyTasks.Repositories.DTOS;

namespace MyTasks.Repositories.Interfaces.IUserDataRepository
{
    public interface IUserDataRepository
    {
        Task AddUserData(UserWithLoginDto data);
    }
}