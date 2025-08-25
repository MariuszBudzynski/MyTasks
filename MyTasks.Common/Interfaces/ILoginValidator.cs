using Microsoft.AspNetCore.Mvc;
using MyTasks.Repositories.DTOS;

namespace MyTasks.Common.Interfaces
{
    public interface ILoginValidator
    {
        Task<IActionResult> ValidateLogin(LoginRequest request);
    }
}