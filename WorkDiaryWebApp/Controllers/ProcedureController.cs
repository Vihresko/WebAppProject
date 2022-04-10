using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WorkDiaryWebApp.Core.Constants;
using WorkDiaryWebApp.Core.Interfaces;

namespace WorkDiaryWebApp.Controllers
{
    [Authorize]
    public class ProcedureController : Controller
    {
        private readonly IProcedureService procedureService;
        private readonly IMemoryCache memoryCash;
        public ProcedureController(IProcedureService _procedureService, IMemoryCache _memoryCash)
        {
            procedureService = _procedureService;
            memoryCash = _memoryCash;
        }

        public async Task<IActionResult> Procedures()
        {
            if(!memoryCash.TryGetValue("Procedures", out var model))
            {
                if (User.IsInRole(UserConstant.Role.ADMINISTRATOR))
                {
                    model = await procedureService.GetAllProceduresAdmin();
                }
                else
                {
                    model = await procedureService.GetAllProcedures();
                }
                memoryCash.Set("Procedures", model, TimeSpan.FromSeconds(5));
            }

            //TempData is needed for Back button 
            TempData["Controller"] = "Procedure";
            TempData["Action"] = "Procedures";

            return View(model);
        }

        public async Task<IActionResult> Procedure(string procedureId)
        {
            if(!memoryCash.TryGetValue("Procedure", out var model))
            {
                model = await procedureService.ProcedureInfo(procedureId);
                memoryCash.Set("Procedure", model, TimeSpan.FromSeconds(5));
            }
            return View(model);
        }
       
    }
}
