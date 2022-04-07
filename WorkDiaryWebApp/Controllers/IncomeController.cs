using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkDiaryWebApp.Core.Constants;
using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.Models;
using WorkDiaryWebApp.Models.Income;
using WorkDiaryWebApp.WorkDiaryDB.Models;

namespace WorkDiaryWebApp.Controllers
{
    public class IncomeController : BaseControllerUser
    {
        private readonly IClientService clientService;
        private readonly IProcedureService procedureService;
        private readonly UserManager<User> userManager;
        private readonly IIncomeService incomeService;
        public IncomeController(IClientService _clientService, IProcedureService _procedureService, UserManager<User> _userManager, IIncomeService _incomeService)
        {
            clientService = _clientService;
            procedureService = _procedureService;
            userManager = _userManager;
            incomeService = _incomeService;
        }
        public async Task<IActionResult> UserIncomes()
        {
            
            var userId = userManager.GetUserId(User);
            var model = await incomeService.GetUnreportedUserIncomes(userId);
            ViewBag.Show = "Unreported";

            return View(model);
        }

        public async Task<IActionResult> UserIncomesHistory()
        {
            var userId = userManager.GetUserId(User);
            var model = await incomeService.GetUserIncomesHistory(userId);
            return View("~/Views/Income/UserIncomes.cshtml",model);
        }

        public async Task<IActionResult> CleanUserDiary()
        {
            var userId = userManager.GetUserId(User);
            var isDone = await incomeService.CleanUserDiary(userId);
            return Redirect("/Income/UserIncomes");
        }

        public async Task<IActionResult> CreateIncome(string clientId)
        {
            var model = await GetWorkModelForView(clientId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddIncomeToClientVisitBag(AddIncomePostModel addWorkmodel)
        {
            
            string userId = userManager.GetUserId(this.User);
            (bool isDone, string? errors) = await incomeService.AddClientProcedureToVisitBag(addWorkmodel, userId);

            var model = await GetWorkModelForView(addWorkmodel.ClientId);

            if (isDone)
            {
                ViewData[MessageConstant.SuccessMessage] = CommonMessage.SUCCESS_MESSAGE;
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = errors;
            }

            return View("~/Views/Income/CreateIncome.cshtml",model);
        }

        public async Task<IActionResult> ShowHistoryOfClient(string clientId)
        {
            TempData["Controller"] = "Income";
            TempData["Action"] = "ShowHistoryOfClient";
            TempData["neededId"] = $"?clientId={clientId}";
            TempData["clientId"] = clientId;
            var model = await incomeService.ShowClientHistory(clientId);
            return View(model);
        }

        public async Task<IActionResult> ShowClientVisitBag(string clientId)
        {
            TempData["Controller"] = "Income";
            TempData["Action"] = "CreateIncome";
            TempData["neededId"] = $"?clientId={clientId}";
            TempData["clientId"] = clientId;
            var model = await incomeService.ShowClientVisitBag(clientId);
            return View(model);
        }

        public async Task<IActionResult> RemoveProcedure(string clientId, string procedureId)
        {
            await incomeService.RemoveProcedureFromVisitBag(clientId, procedureId);
            return Redirect($"/Income/ShowClientVisitBag?clientId={clientId}");
          
        }

        [HttpPost]
        public async Task<IActionResult> Pay(string clientId, decimal totalPrice)
        {
            var proceduresInfo = await incomeService.ShowClientVisitBag(clientId);
            string userId = userManager.GetUserId(this.User);

            string document = await incomeService.GetInfoForPayment(clientId, totalPrice, userId, proceduresInfo);
            var model = new PayPostModel()
            {
                Description = document,
                ClientId = clientId,
                UserId = userId,
                Value = totalPrice
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmPay(PayPostModel model)
        {
            var result = await incomeService.CompleetePayment(model);
            if (result)
            {
                ViewData[MessageConstant.SuccessMessage] = CommonMessage.SUCCESS_MESSAGE;
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = "error!";
            }
            //TODO:Redirect correctly
            // return Redirect($"/Income/ShowClientVisitBag?clientId={model.ClientId}");

            TempData["Controller"] = "Income";
            TempData["Action"] = "CreateIncome";
            TempData["neededId"] = $"?clientId={model.ClientId}";
            TempData["clientId"] = model.ClientId;
            var modelReturn = await incomeService.ShowClientVisitBag(model.ClientId);
            return View("~/Views/Income/ShowClientVisitBag.cshtml", modelReturn);
        }

        private async Task<WorkModel> GetWorkModelForView(string clientId)
        {
            //TempData is needed for Back button 
            TempData["Controller"] = "Income";
            TempData["Action"] = "CreateIncome";
            TempData["neededId"] = $"?clientId={clientId}";

            var clientModel = await clientService.ClientInfo(clientId);
            var proceduresModel = await procedureService.GetAllProcedures();
            var model = new WorkModel(clientModel, proceduresModel);
            return model;
        }

    }
}
