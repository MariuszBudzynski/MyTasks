using Microsoft.AspNetCore.Mvc;
using MyTasks.Pages;
using MyTasks.Repositories.Interfaces.ILoginRepository;

namespace MyTasks.Services.Interfaces
{
    public interface ILoginValidator
    {
        Task<IActionResult> ValidateLogin(LoginModel.LoginRequest request);
    }
}