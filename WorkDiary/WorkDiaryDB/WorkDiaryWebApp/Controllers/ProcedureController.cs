using Microsoft.AspNetCore.Mvc;
using WorkDiaryWebApp.Models.Procedure;

namespace WorkDiaryWebApp.Controllers
{
    public class ProcedureController : Controller
    {
        public IActionResult Procedures()
        {
            return View();
        }

        public IActionResult Procedure()
        {
            return View();
        }
        public IActionResult AddProcedure()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProcedure(AddProcedureModel model)
        {
            return View();
        }

       

    }
}
