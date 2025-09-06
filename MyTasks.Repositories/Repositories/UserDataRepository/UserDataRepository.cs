using Microsoft.EntityFrameworkCore;
using MyTasks.DbOperations.Interface;
using MyTasks.Models.Models;
using MyTasks.Repositories.DTOS;
using MyTasks.Repositories.Interfaces.IUserDataRepository;
using MyTasks.Repositories.Services;

namespace MyTasks.Repositories.Repositories.UserDataRepository
{
    public class UserDataRepository : IUserDataRepository
    {
        private readonly IUserOperationsRepository _repository;

        public UserDataRepository(IUserOperationsRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserResponseDto> AddUserDataAsync(UserWithLoginDto data)
        {
            var loginId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var hashedPassword = PasswordHasher.HashPassword(data.PasswordHash);

            var user = new UserModel()
            {
                Id = userId,
                FullName = data.FullName,
                LoginId = loginId,
            };

            var login = new LoginModel()
            {
                Id = loginId,
                Username = data.Username,
                PasswordHash = hashedPassword,
                Type = data.Type,
                UserId = userId,
            };

            await _repository.AddUserAndLoginAsync(user, login);

            return new UserResponseDto(
                user.Id,
                user.FullName,
                login.Username,
                login.Type,
                user.IsDeleted
            );
        }

        public async Task UpdateUserDataAsync(Guid? userId, UserWithLoginDto data)
        {
            var hashedPassword = PasswordHasher.HashPassword(data.PasswordHash);

            var userData = await _repository.GetUserAndLoginDataByIDAsync(userId);
            if (userData == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            userData.FullName = data.FullName;
            userData.LoginModel.Username = data.Username;
            userData.LoginModel.PasswordHash = hashedPassword;
            userData.LoginModel.Type = data.Type;

            await _repository.UpdateUserDataAsync(userData);
        }

        public async Task HardDeleteUserDataAsync(Guid? userId)
        {
            var userData = await _repository.GetUserAndLoginDataByIDAsync(userId);
            if (userData == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            if (userData.LoginModel?.Type == UserType.Admin)
            {
                throw new InvalidOperationException("Cannot delete an Admin user.");
            }

            await _repository.DeleteUserDataAsync(userData);
        }

        public async Task SoftDeleteUserDataAsync(Guid? userId)
        {
            var userData = await _repository.GetUserAndLoginDataByIDAsync(userId);
            if (userData == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            if (userData.LoginModel?.Type == UserType.Admin)
            {
                throw new InvalidOperationException("Cannot delete an Admin user.");
            }

            userData.IsDeleted = true;
            await _repository.UpdateUserDataAsync(userData);
        }

        public async Task<ICollection<UserResponseDto>> GetAllUserDataAsync()
        {
            var data = _repository.GetAllUserAndLoginData();

            if (!await data.AnyAsync())
            {
                throw new InvalidOperationException("No data found");
            }

            return await  data.Select(user => new UserResponseDto(
                user.Id,
                user.FullName,
                user.LoginModel != null ? user.LoginModel.Username : "",
                user.LoginModel != null ? user.LoginModel.Type : UserType.Regular,
                user.IsDeleted
            )).ToListAsync();
        }

        public async Task<UserResponseDto?> GetUserDataAsync(Guid id)
        {
            var user = await _repository.GetUserByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            return new UserResponseDto(
                user.Id,
                user.FullName,
                user.LoginModel.Username,
                user.LoginModel.Type,
                user.IsDeleted
            );
        }
    }
}