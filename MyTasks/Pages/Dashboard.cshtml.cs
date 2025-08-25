using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTasks.Repositories.Interfaces.IDashboardRepository;
using MyTasks.Services.Interfaces;
using Newtonsoft.Json;

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
        public async Task OnGetAsync()
        {
            var userName = _iJwtHelper.GetLoggedInUserName();

            if (!string.IsNullOrEmpty(userName))
            {
                var data = await _repository.GetProjectsData(userName);
                ViewData["DashboardData"] = JsonConvert.SerializeObject(data);
            }
        }
    }
}
