using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTasks.Repositories.Interfaces.ILoginRepository;
using MyTasks.Repositories.Repositories.LoginRepository;
using MyTasks.Services.Interfaces;

namespace MyTasks.Pages
{
    public class LoginModel : PageModel
    {
        
        private readonly ILoginValidator _loginValidator;
        private readonly IConfiguration _config;

        public LoginModel
            (IConfiguration config,
            ILoginValidator loginValidator)
        {
           
            _config = config;
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
