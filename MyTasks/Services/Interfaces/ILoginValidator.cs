using Microsoft.AspNetCore.Mvc;
using MyTasks.Pages;

namespace MyTasks.Services.Interfaces
{
    public interface ILoginValidator
    {
        Task<IActionResult> ValidateLogin(LoginModel.LoginRequest request);
    }
}