using MyTasks.DbOperations.Context;
using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;

namespace MyTasks.DbOperations.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAndLogin(UserModel user, LoginModel login)
        {
            await _context.AddRangeAsync(user, login);
            await _context.SaveChangesAsync();
        }
    }
}
