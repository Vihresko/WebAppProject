using Microsoft.AspNetCore.Mvc;
using WorkDiaryWebApp.Core.Interfaces;

namespace WorkDiaryWebApp.Controllers
{
    public class ProcedureController : Controller
    {
        private readonly IProcedureService procedureService;
        public ProcedureController(IProcedureService _procedureService)
        {
            procedureService = _procedureService;
        }
        public async Task<IActionResult> Procedures()
        {
            var model = await procedureService.GetAllProcedures();

            //TempData is needed for Back button 
            TempData["Controller"] = "Procedure";
            TempData["Action"] = "Procedures";

            return View(model);
        }

        public async Task<IActionResult> Procedure(string procedureId)
        {
            var model = await procedureService.ProcedureInfo(procedureId);
           
            return View(model);
        }
       
    }
}
