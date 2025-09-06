using MyTasks.Models.Models;

namespace MyTasks.Repositories.Interfaces.ILoginRepository
{
    public interface ILoginRepository
    {
        Task<LoginModel?> GetUserLoginDataByIdAsync(Guid Id);
        Task<LoginModel?> GetUserLoginDataByUserNameAsync(string userName);
        Task<UserModel?> GetUserDataByIdAsync(Guid Id);
    }
}