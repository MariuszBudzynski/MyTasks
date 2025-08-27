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

        public async Task<LoginModel?> GetUserLoginDataById(Guid Id)
        {
            return await _loginRepository.GetById(Id);
        }

        public async Task<UserModel?> GetUserDataById(Guid Id)
        {
            return await _userRepository.GetById(Id);
        }

        public async Task<LoginModel?> GetUserLoginDataByUserName(string userName)
        {
            return await _loginRepository.GetByUserName<LoginModel>(userName);
        }
    }
}