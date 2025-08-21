using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyTasks.Pages
{
    public class DashboardModel : PageModel
    {
        public async Task<IActionResult> OnGetDashboardAsync()
        {
            return new JsonResult(new { success = true }); //add proper implementaion later
        }
    }
}
