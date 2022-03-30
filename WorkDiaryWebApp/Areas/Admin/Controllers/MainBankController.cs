using Microsoft.AspNetCore.Mvc;
using WorkDiaryWebApp.Core.Interfaces;

namespace WorkDiaryWebApp.Areas.Admin.Controllers
{
    public class MainBankController : BaseControllerAdmin
    {
        private readonly IMainBankService mainBankService;
        public MainBankController(IMainBankService _mainBankService)
        {
            mainBankService = _mainBankService;
        }
        public async Task<IActionResult> MainBank()
        {
            var model = await mainBankService.GetMainBankInfo();
            return View(model);
        }
    }
}
