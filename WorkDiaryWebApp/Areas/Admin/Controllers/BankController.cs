using Microsoft.AspNetCore.Mvc;

namespace WorkDiaryWebApp.Areas.Admin.Controllers
{
    public class BankController : BaseController
    {
        public IActionResult MainBank()
        {
            return View();
        }
    }
}
