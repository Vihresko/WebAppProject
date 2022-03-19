using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkDiaryWebApp.Core.Constants;
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

        public IActionResult UserBank()
        {
            var userId = userManager.GetUserId(User);
            var model = bankService.GetUserBankBalance(userId);
            return View(model);
        }

        public IActionResult MainBank()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ReportMoney(ReportMoneyPostModel model)
        {
            (bool isDone, string message) = bankService.ReportMoney(model);
            return Redirect($"/Bank/UserBank");
        }

    }
}
