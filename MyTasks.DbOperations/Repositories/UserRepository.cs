using Microsoft.EntityFrameworkCore;
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
        public async Task<UserModel?> GetUserById(Guid id)
        {
            return await _context
                .User.Where(u => u.Id == id)
                .Include(u => u.LoginModel).FirstOrDefaultAsync();
        }

        public async Task<UserModel?> GetUserAndLoginDataByID(Guid? userId)
        {
            return await _context.User
                .Where(u => u.Id == userId)
                .Include(u => u.LoginModel)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<UserModel>> GetAllUserAndLoginData()
        {
            return await _context.User
                .Include(u => u.LoginModel).ToListAsync();
        }

        public async Task UpdateUserData(UserModel user)
        {
             _context.Update(user);
             await _context.SaveChangesAsync();
        }

        public async Task DeleteUserData(UserModel user)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
