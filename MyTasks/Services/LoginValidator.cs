using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTasks.Repositories.Interfaces.ILoginRepository;
using MyTasks.Services.Interfaces;
using static MyTasks.Pages.LoginModel;

namespace MyTasks.Services
{
    public class LoginValidator : PageModel, ILoginValidator
    {
        private readonly ILoginRepository _loginRepository;
        public LoginValidator(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
        public async Task<IActionResult> ValidateLogin(LoginRequest request)
        {
            try
            {
                var user = await _loginRepository.GetUserLoginDataByUserName(request.Username);
                if (user == null)
                {
                    return NotFound(new { success = false, message = "User not found" });
                }

                if (request.Username == user.Username && request.Password == user.PasswordHash)
                {

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Internal server error." });
            }

            return new JsonResult(new { success = true, message = "Token valid" });
        }
    }
}
