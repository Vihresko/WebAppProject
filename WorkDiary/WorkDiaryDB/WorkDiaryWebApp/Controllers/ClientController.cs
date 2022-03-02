using Microsoft.AspNetCore.Mvc;
using WorkDiaryCore.Constraints.Constants;
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
        public IActionResult AddClient(AddClientModel model)
        {
            bool isDone = false;
            if (isDone)
            {
                ViewData[MessageConstant.SuccessMessage] = "success";
            }
            //TODO: Create correct errors
            else
            {
                ViewData[MessageConstant.WarningMessage] = "warning";
                ViewData[MessageConstant.ErrorMessage] = "error";
            }
            return View();
        }


    }
}
