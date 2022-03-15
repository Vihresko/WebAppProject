using Microsoft.AspNetCore.Mvc;

namespace WorkDiaryWebApp.Controllers
{
    public class BankController : Controller
    {
        public IActionResult UserBank()
        {

            return View();
        }

        public IActionResult MainBank()
        {
            return View();
        }

    }
}
