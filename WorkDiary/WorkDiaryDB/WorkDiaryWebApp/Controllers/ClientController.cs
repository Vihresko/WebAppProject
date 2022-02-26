using Microsoft.AspNetCore.Mvc;
using WorkDiaryWebApp.Models.Client;

namespace WorkDiaryWebApp.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Clients()
        {
            return View();
        }
        public IActionResult Client()
        {
            return View();
        }

        public IActionResult AddClient()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddClient(AddClientPostModel model)
        {

            return View();
        }


    }
}
