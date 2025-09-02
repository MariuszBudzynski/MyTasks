using Microsoft.EntityFrameworkCore;
using MyTasks.DbOperations.Context;
using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;

namespace MyTasks.DbOperations.Repositories
{
    public class UserOperationsRepository : IUserOperationsRepository
    {
        private readonly AppDbContext _context;
        public UserOperationsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAndLoginAsync(UserModel user, LoginModel login)
        {
            await _context.AddRangeAsync(user, login);
            await _context.SaveChangesAsync();
        }
        public async Task<UserModel?> GetUserByIdAsync(Guid id)
        {
            return await _context
                .User.Where(u => u.Id == id)
                .Include(u => u.LoginModel).FirstOrDefaultAsync();
        }

        public async Task<UserModel?> GetUserAndLoginDataByIDAsync(Guid? userId)
        {
            return await _context.User
                .Where(u => u.Id == userId)
                .Include(u => u.LoginModel)
                .FirstOrDefaultAsync();
        }

        public IQueryable<UserModel> GetAllUserAndLoginData()
        {
            return _context.User
                .Include(u => u.LoginModel)
                .AsQueryable();
        }

        public async Task UpdateUserDataAsync(UserModel user)
        {
             _context.Update(user);
             await _context.SaveChangesAsync();
        }

        public async Task DeleteUserDataAsync(UserModel user)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
