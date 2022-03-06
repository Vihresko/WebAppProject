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
            return View(model);
        }

        public IActionResult Procedure(string procedureId)
        {
            var model = procedureService.ProcedureInfo(procedureId);
            return View(model);
        }
        public IActionResult AddProcedure()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProcedure(AddProcedureModel model)
        {
           
            (bool isDone, string errors) = procedureService.AddNewProcedure(model);

            if (isDone)
            {
                ViewData[MessageConstant.SuccessMessage] = "success";
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = errors;
            }
            return View();
        }

        public IActionResult EditProcedure(string procedureId)
        {
            var model = procedureService.ProcedureInfo(procedureId);
            return View(model);
        }

        [HttpPost]
        public IActionResult EditProcedure(ShowProcedureModel model)
        {
            (bool isDone, string errors) = procedureService.EditProcedure(model);

            if (isDone)
            {
                ViewData[MessageConstant.SuccessMessage] = "success";
            }
            else if(errors == CommonMessage.NoChangesMessage)
            {
                ViewData[MessageConstant.WarningMessage] = CommonMessage.NoChangesMessage;
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = errors;
            }
            return View(model);
        }
    }
}
