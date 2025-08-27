using MyTasks.Models.Models;

namespace MyTasks.DbOperations.Interface
{
    public interface IUserRepository
    {
        Task AddUserAndLogin(UserModel user, LoginModel login);
    }
}