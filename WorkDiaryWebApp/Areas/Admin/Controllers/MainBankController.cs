using Microsoft.AspNetCore.Mvc;
using WorkDiaryWebApp.Core.Interfaces;

namespace WorkDiaryWebApp.Areas.Admin.Controllers
{
    public class MainBankController : BaseController
    {
        private readonly IMainBankService mainBankService;
        public MainBankController(IMainBankService _mainBankService)
        {
            mainBankService = _mainBankService;
        }
        public IActionResult MainBank()
        {
            var model = mainBankService.GetMainBankInfo();
            return View(model);
        }
    }
}
