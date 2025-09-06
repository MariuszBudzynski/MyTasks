using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;
using MyTasks.Repositories.Interfaces.ILoginRepository;

namespace MyTasks.Repositories.Repositories.LoginRepository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IDbRepository<LoginModel> _loginRepository;
        private readonly IDbRepository<UserModel> _userRepository;

        public LoginRepository(
            IDbRepository<LoginModel> repository,
            IDbRepository<UserModel> userRepository)
        {
            _loginRepository = repository;
            _userRepository = userRepository;
        }

        public async Task<LoginModel?> GetUserLoginDataByIdAsync(Guid Id)
        {
            return await _loginRepository.GetByIdAsync(Id);
        }

        public async Task<UserModel?> GetUserDataByIdAsync(Guid Id)
        {
            return await _userRepository.GetByIdAsync(Id);
        }

        public async Task<LoginModel?> GetUserLoginDataByUserNameAsync(string userName)
        {
            return await _loginRepository.GetByUserNameAsync<LoginModel>(userName);
        }
    }
}