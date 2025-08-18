using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyTasks.Pages
{
    public class LoginModel : PageModel
    {
        public async Task OnGetAsync()
        {
            //removed later
            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostLoginAsync([FromBody]LoginRequest request) 
        {
            //implement validtion logic later
            await Task.CompletedTask;
            return new JsonResult(new { success = true, message = "Token valid" });
        }
    }
}
