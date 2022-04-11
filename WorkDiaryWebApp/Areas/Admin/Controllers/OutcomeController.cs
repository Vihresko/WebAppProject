using Microsoft.AspNetCore.Mvc;
using WorkDiaryWebApp.Core.Constants;
using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.Models.Outcome;

namespace WorkDiaryWebApp.Areas.Admin.Controllers
{
    public class OutcomeController : BaseControllerAdmin
    {
        private readonly IOutcomeService outcomeService;
        public OutcomeController(IOutcomeService _outcomeService)
        {
            outcomeService = _outcomeService;
        }
        public async Task<IActionResult> Outcomes()
        {
            var model = await outcomeService.GetAllOutcomes();
            return View(model);
        }

        public async Task<IActionResult> AddOutcome()
        {
            var vault = await outcomeService.GetMainBankVault();
            var model = new AddOutcomeModel()
            {
                MainBankVault = vault
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddOutcome(AddOutcomeModel addOutComeModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addOutComeModel);
            }

            (bool isDone, string errors) = await outcomeService.AddOutcome(addOutComeModel);
            if (isDone)
            {
                ViewData[MessageConstant.SuccessMessage] = CommonMessage.SUCCESS_MESSAGE;
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = errors;
            }
            var vault = await outcomeService.GetMainBankVault();
            var model = new AddOutcomeModel()
            {
                MainBankVault = vault
            };
            return View(model);
        }
    }
}
