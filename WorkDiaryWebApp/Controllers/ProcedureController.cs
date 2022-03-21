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
        public IActionResult AddProcedure()
        {
            return View(new AddProcedureModel());
        }

        [HttpPost]
        public IActionResult AddProcedure(AddProcedureModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
           
            (bool isDone, string? errors) = procedureService.AddNewProcedure(model);

            if (!isDone)
            {
                var splitedErrors = errors.ToString().Split(Environment.NewLine);

                foreach (var error in splitedErrors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            if (isDone)
            {
                ViewData[MessageConstant.SuccessMessage] = "Success!";
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = "Invalid data!";
            }
            return View(model);
        }

        public IActionResult EditProcedure(string procedureId)
        {
            var model = procedureService.ProcedureInfo(procedureId);
            return View(model);
        }

        [HttpPost]
        public IActionResult EditProcedure(ShowProcedureModel model)
        {
            (bool isDone, string? errors) = procedureService.EditProcedure(model);

            if (isDone)
            {
                ViewData[MessageConstant.SuccessMessage] = "success";
            }
            else if(errors == CommonMessage.NO_CHANGES_MESSAGE)
            {
                ViewData[MessageConstant.WarningMessage] = CommonMessage.NO_CHANGES_MESSAGE;
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = errors;
            }
            return View(model);
        }
    }
}
