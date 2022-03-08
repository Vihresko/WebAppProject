using Microsoft.AspNetCore.Mvc;

namespace WorkDiaryWebApp.Controllers
{
    public class OutcomeController : Controller
    {
        public IActionResult UserOutcomes()
        {
            return View();
        }

        public IActionResult TotalOutcomes()
        {
            return View();
        }
    }
}
