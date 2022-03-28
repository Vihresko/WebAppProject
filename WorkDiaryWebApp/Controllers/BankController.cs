using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.Models.Bank;
using WorkDiaryWebApp.WorkDiaryDB.Models;

namespace WorkDiaryWebApp.Controllers
{
    public class BankController : Controller
    {
        private readonly IBankService bankService;
        private readonly UserManager<User> userManager;
        public BankController(IBankService _bankService, UserManager<User> _userManager)
        {
            bankService = _bankService;
            userManager = _userManager;
        }

        public async Task <IActionResult> UserBank()
        {
            var userId = userManager.GetUserId(User);
            var model = await bankService.GetUserBankBalance(userId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ReportMoney(ReportMoneyPostModel model)
        {
            (bool isDone, string message) = await bankService.ReportMoney(model);
            
            return Redirect($"/Bank/UserBank");
        }

    }
}
