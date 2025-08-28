using MyTasks.Models.Models;

namespace MyTasks.DbOperations.Interface
{
    public interface IUserRepository
    {
        Task AddUserAndLogin(UserModel user, LoginModel login);
        Task<UserModel?> GetUserAndLoginDataByID(Guid? userId);
        Task UpdateUserData(UserModel user);
        Task DeleteUserData(UserModel user);
        Task<ICollection<UserModel>> GetAllUserAndLoginData();
    }
}