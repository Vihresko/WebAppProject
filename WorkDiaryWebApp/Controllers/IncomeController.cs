using Microsoft.AspNetCore.Mvc;

namespace WorkDiaryWebApp.Controllers
{
    public class IncomeController : Controller
    {
        public IActionResult UserIncomes()
        {
            return View();
        }

        public IActionResult TotalIncomes()
        {
            return View();
        }

        
    }
}
