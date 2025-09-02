using MyTasks.Models.Models;

namespace MyTasks.DbOperations.Interface
{
    public interface IUserOperationsRepository
    {
        Task AddUserAndLoginAsync(UserModel user, LoginModel login);
        Task<UserModel?> GetUserAndLoginDataByIDAsync(Guid? userId);
        Task UpdateUserDataAsync(UserModel user);
        Task DeleteUserDataAsync(UserModel user);
        IQueryable<UserModel> GetAllUserAndLoginData();
        Task<UserModel?> GetUserByIdAsync(Guid id);
    }
}