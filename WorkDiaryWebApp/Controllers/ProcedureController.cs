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
        public async Task<IActionResult> Procedures()
        {
            ListFromProcedures model = null;
            if (User.IsInRole(UserConstant.Role.ADMINISTRATOR))
            {
                model = await procedureService.GetAllProceduresAdmin();
            }
            else
            {
                model = await procedureService.GetAllProcedures();
            }

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
