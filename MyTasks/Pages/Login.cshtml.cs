using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTasks.Repositories.Interfaces.ILoginRepository;
using MyTasks.Repositories.Repositories.LoginRepository;

namespace MyTasks.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILoginRepository _loginRepository;
        public LoginModel(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
        public async Task OnGetAsync()
        {
            //removed later
            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostLoginAsync([FromBody]LoginRequest request) 
        {
            //implement validtion logic later
            try
            {
                var user = await _loginRepository.GetUserLoginDataByUserName(request.Username);
                if (user == null)
                {
                    return NotFound(new { success = false, message = "User not found" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Internal server error." });
            }

            return new JsonResult(new { success = true, message = "Token valid" });
        }
        public record LoginRequest(string Username, string Password);
    }
}
