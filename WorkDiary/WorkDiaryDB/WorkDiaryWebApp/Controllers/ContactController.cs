using Microsoft.AspNetCore.Mvc;

namespace WorkDiaryWebApp.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Contacts()
        {
            return View();
        }
    }
}
