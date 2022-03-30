using Microsoft.AspNetCore.Mvc;

namespace WorkDiaryWebApp.Areas.Admin.Controllers
{
    public class HomeController : BaseControllerAdmin
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
