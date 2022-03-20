using Microsoft.AspNetCore.Mvc;

namespace WorkDiaryWebApp.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
