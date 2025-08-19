using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTasks.Services.Interfaces;

namespace MyTasks.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILoginValidator _loginValidator;
        
        public LoginModel
            (ILoginValidator loginValidator)
        {
            _loginValidator = loginValidator;
        }
        public async Task OnGetAsync()
        {
            //removed later
            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostLoginAsync([FromBody]LoginRequest request)
        {
            //implement validtion logic later
            return await _loginValidator.ValidateLogin(request);
        }

        public record LoginRequest(string Username, string Password);
    }
}
