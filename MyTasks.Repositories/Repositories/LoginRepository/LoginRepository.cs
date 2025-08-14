using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;
using MyTasks.Repositories.Interfaces.ILoginRepository;

namespace MyTasks.Repositories.Repositories.LoginRepository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IDbRepository<LoginModel> _repository;
        public LoginRepository(IDbRepository<LoginModel> repository)
        {
            _repository = repository;
        }

        public async Task<LoginModel> GetUserLoginDataById(Guid Id)
        {
            var user = await _repository.GetById(Id);
            if (user == null)
                throw new InvalidOperationException($"User with Id {Id} not found.");

            return user;
        }
    }
}