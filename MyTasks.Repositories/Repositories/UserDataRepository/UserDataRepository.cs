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
        private readonly IUserRepository _repository;

        public UserDataRepository(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserResponseDto> AddUserData(UserWithLoginDto data)
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

            await _repository.AddUserAndLogin(user, login);

            return new UserResponseDto(
                user.Id,
                user.FullName,
                login.Username,
                login.Type,
                user.IsDeleted
            );
        }

        public async Task UpdateUserData(Guid? userId, UserWithLoginDto data)
        {
            var hashedPassword = PasswordHasher.HashPassword(data.PasswordHash);

            var userData = await _repository.GetUserAndLoginDataByID(userId);
            if (userData == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            userData.FullName = data.FullName;
            userData.LoginModel.Username = data.Username;
            userData.LoginModel.PasswordHash = hashedPassword;
            userData.LoginModel.Type = data.Type;

            await _repository.UpdateUserData(userData);
        }

        public async Task HardDeleteUserData(Guid? userId)
        {
            var userData = await _repository.GetUserAndLoginDataByID(userId);
            if (userData == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            if (userData.LoginModel?.Type == UserType.Admin)
            {
                throw new InvalidOperationException("Cannot delete an Admin user.");
            }

            await _repository.DeleteUserData(userData);
        }

        public async Task SoftDeleteUserData(Guid? userId)
        {
            var userData = await _repository.GetUserAndLoginDataByID(userId);
            if (userData == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            if (userData.LoginModel?.Type == UserType.Admin)
            {
                throw new InvalidOperationException("Cannot delete an Admin user.");
            }

            userData.IsDeleted = true;
            await _repository.UpdateUserData(userData);
        }

        public async Task<ICollection<UserResponseDto>> GetAllUserData()
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

        public async Task<UserResponseDto?> GetUserData(Guid id)
        {
            var user = await _repository.GetUserById(id);
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