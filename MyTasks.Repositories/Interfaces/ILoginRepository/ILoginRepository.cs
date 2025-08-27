using MyTasks.Models.Models;

namespace MyTasks.Repositories.Interfaces.ILoginRepository
{
    public interface ILoginRepository
    {
        Task<LoginModel?> GetUserLoginDataById(Guid Id);
        Task<LoginModel?> GetUserLoginDataByUserName(string userName);
        Task<UserModel?> GetUserDataById(Guid Id);
    }
}