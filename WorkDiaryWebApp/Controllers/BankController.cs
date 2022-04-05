using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkDiaryWebApp.Core.Constants;
using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.Models.Bank;
using WorkDiaryWebApp.WorkDiaryDB.Models;

namespace WorkDiaryWebApp.Controllers
{
    public class BankController : BaseControllerUser
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

            if (isDone)
            {
                ViewData[MessageConstant.SuccessMessage] = CommonMessage.SUCCESS_MESSAGE;
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = message;
            }

           var userId = userManager.GetUserId(User);
           var model1 = await bankService.GetUserBankBalance(userId);
           return View("~/Views/Bank/UserBank.cshtml",model1);
        }

    }
}
