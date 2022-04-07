using Microsoft.AspNetCore.Mvc;
using WorkDiaryWebApp.Core.Constants;
using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.Models.Procedure;

namespace WorkDiaryWebApp.Areas.Admin.Controllers
{
    public class ProcedureController : BaseControllerAdmin
    {
        private readonly IProcedureService procedureService;
        public ProcedureController(IProcedureService _procedureService)
        {
            procedureService = _procedureService;
        }
        public IActionResult AddProcedure()
        {
            return View(new AddProcedureModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddProcedure(AddProcedureModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            (bool isDone, string? errors) =  await procedureService.AddNewProcedure(model);

            if (!isDone && errors?.Length > 0)
            {
                var splitedErrors = errors.ToString().Split(Environment.NewLine);

                foreach (var error in splitedErrors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            if (isDone)
            {
                ViewData[MessageConstant.SuccessMessage] = CommonMessage.SUCCESS_MESSAGE;
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = CommonMessage.INVALID_DATA;
            }
            return View(model);
        }

        public async Task<IActionResult> EditProcedure(string procedureId)
        {
            var model = await procedureService.ProcedureInfo(procedureId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProcedure(ShowProcedureModel model)
        {
            (bool isDone, string? errors) = await procedureService.EditProcedure(model);

            if (isDone)
            {
                ViewData[MessageConstant.SuccessMessage] = CommonMessage.SUCCESS_MESSAGE;
            }
            else if (errors == CommonMessage.NO_CHANGES_MESSAGE)
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
