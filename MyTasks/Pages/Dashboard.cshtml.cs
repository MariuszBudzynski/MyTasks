using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTasks.Repositories.Interfaces.IDashboardRepository;
using MyTasks.Services.Interfaces;

namespace MyTasks.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly IDashboardRepository _repository;
        private readonly IJwtHelper _iJwtHelper;

        public DashboardModel(IDashboardRepository repository, IJwtHelper iJwtHelper)
        {
            _repository = repository;
            _iJwtHelper = iJwtHelper;
        }
        public async Task<IActionResult> OnGetDashboardAsync()
        {
            var userName = _iJwtHelper.GetLoggedInUserName();

            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest("User not loged in");
            }

            var data = await _repository.GetProjectsData(userName);
            return new JsonResult(new { success = true, data });
        }
    }
}
