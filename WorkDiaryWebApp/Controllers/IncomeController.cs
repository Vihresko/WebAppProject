using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkDiaryWebApp.Core.Constants;
using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.Core.Services;
using WorkDiaryWebApp.Models;
using WorkDiaryWebApp.Models.Income;
using WorkDiaryWebApp.WorkDiaryDB.Models;

namespace WorkDiaryWebApp.Controllers
{
    public class IncomeController : Controller
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
        public IActionResult UserIncomes()
        {
            return View();
        }

        public IActionResult TotalIncomes()
        {
            return View();
        }

        public IActionResult CreateIncome(string clientId)
        {
            var model = GetWorkModelForView(clientId);
            return View(model);
        }

        [HttpPost]
        public IActionResult AddIncomeToClientVisitBag(AddIncomePostModel addWorkmodel)
        {
            
            string userId = userManager.GetUserId(this.User);
            (bool isDone, string? errors) = incomeService.AddClientProcedureToVisitBag(addWorkmodel, userId);

            var model = GetWorkModelForView(addWorkmodel.ClientId);

            if (isDone)
            {
                ViewData[MessageConstant.SuccessMessage] = "success";
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = errors;
            }

            return View("~/Views/Income/CreateIncome.cshtml",model);
        }

        public IActionResult ShowHistoryOfClient(string clientId)
        {
            TempData["Controller"] = "Income";
            TempData["Action"] = "ShowHistoryOfClient";
            TempData["neededId"] = $"?clientId={clientId}";
            var model = incomeService.ShowClientHistory(clientId);
            return View(model);
        }

        public IActionResult ShowClientVisitBag(string clientId)
        {
            TempData["Controller"] = "Income";
            TempData["Action"] = "CreateIncome";
            TempData["neededId"] = $"?clientId={clientId}";
            TempData["clientId"] = clientId;
            var model = incomeService.ShowClientVisitBag(clientId);
            return View(model);
        }

        public IActionResult RemoveProcedure(string clientId, string procedureId)
        {
            incomeService.RemoveProcedureFromVisitBag(clientId, procedureId);
            return Redirect($"/Income/ShowClientVisitBag?clientId={clientId}");
          
        }

        [HttpPost]
        public IActionResult Pay(string clientId, decimal totalPrice)
        {
            var proceduresInfo = incomeService.ShowClientVisitBag(clientId);
            string userId = userManager.GetUserId(this.User);
            //TODO: userId do nothing?
            string document = incomeService.GetInfoForPayment(clientId, totalPrice, userId, proceduresInfo);
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
        public IActionResult ConfirmPay(PayPostModel model)
        {
            var result = incomeService.CompleetePayment(model);

            //TODO:Redirect correctly
            return Redirect($"/Income/ShowClientVisitBag?clientId={model.ClientId}");
        }

        private WorkModel GetWorkModelForView(string clientId)
        {
            //TempData is needed for Back button 
            TempData["Controller"] = "Income";
            TempData["Action"] = "CreateIncome";
            TempData["neededId"] = $"?clientId={clientId}";

            var clientModel = clientService.ClientInfo(clientId);
            var proceduresModel = procedureService.GetAllProcedures();
            var model = new WorkModel(clientModel, proceduresModel);
            return model;
        }

    }
}
