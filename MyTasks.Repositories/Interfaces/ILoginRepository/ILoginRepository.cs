using MyTasks.Models.Models;

namespace MyTasks.Repositories.Interfaces.ILoginRepository
{
    public interface ILoginRepository
    {
        Task<LoginModel> GetUserLoginDataById(Guid Id);
    }
}