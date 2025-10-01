using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;

namespace MyTasks.DbOperations.InMemory
{
    public class InMemoryUserOperationsRepository : IUserOperationsRepository
    {
        private readonly InMemoryDbContext _context;

        public InMemoryUserOperationsRepository(InMemoryDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAndLoginAsync(UserModel user, LoginModel login)
        {
            login.UserId = user.Id;
            user.LoginModel = login;

            _context.Users.Add(user);
            _context.Logins.Add(login);

            await Task.CompletedTask;
        }

        public async Task<UserModel?> GetUserByIdAsync(Guid id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.LoginModel = _context.Logins.FirstOrDefault(l => l.UserId == user.Id);
            }

            return await Task.FromResult(user);
        }

        public async Task<UserModel?> GetUserAndLoginDataByIDAsync(Guid? userId)
        {
            if (userId == null) return null;

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                user.LoginModel = _context.Logins.FirstOrDefault(l => l.UserId == user.Id);
            }

            return await Task.FromResult(user);
        }

        public IQueryable<UserModel> GetAllUserAndLoginData()
        {
            foreach (var user in _context.Users)
            {
                user.LoginModel = _context.Logins.FirstOrDefault(l => l.UserId == user.Id);
            }

            return _context.Users.AsQueryable();
        }

        public async Task UpdateUserDataAsync(UserModel user)
        {
            var existing = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (existing != null)
            {
                existing.FullName = user.FullName;
                existing.LoginId = user.LoginId;
                existing.LoginModel = user.LoginModel;
                existing.IsDeleted = user.IsDeleted;
            }

            await Task.CompletedTask;
        }

        public async Task DeleteUserDataAsync(UserModel user)
        {
            _context.Users.RemoveAll(u => u.Id == user.Id);
            _context.Logins.RemoveAll(l => l.UserId == user.Id);

            await Task.CompletedTask;
        }
    }
}