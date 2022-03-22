using Microsoft.AspNetCore.Mvc;
using WorkDiaryWebApp.Core.Constants;
using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.Models.Procedure;

namespace WorkDiaryWebApp.Controllers
{
    public class ProcedureController : Controller
    {
        private readonly IProcedureService procedureService;
        public ProcedureController(IProcedureService _procedureService)
        {
            procedureService = _procedureService;
        }
        public IActionResult Procedures()
        {
            var model = procedureService.GetAllProcedures();

            //TempData is needed for Back button 
            TempData["Controller"] = "Procedure";
            TempData["Action"] = "Procedures";

            return View(model);
        }

        public IActionResult Procedure(string procedureId)
        {
            var model = procedureService.ProcedureInfo(procedureId);
           
            return View(model);
        }
       
    }
}
