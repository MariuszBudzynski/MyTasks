using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTasks.Common.Interfaces;
using MyTasks.Repositories.DTOS;

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

        public async Task<IActionResult> OnPostLoginAsync([FromBody]LoginRequest request)
        {
            return await _loginValidator.ValidateLogin(request);
        }
    }
}
