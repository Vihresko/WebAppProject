using Microsoft.AspNetCore.Mvc;
using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.Models;

namespace WorkDiaryWebApp.Controllers
{
    public class IncomeController : Controller
    {
        private readonly IClientService clientService;
        private readonly IProcedureService procedureService;
        public IncomeController(IClientService _clientService, IProcedureService _procedureService)
        {
            clientService = _clientService;
            procedureService = _procedureService;
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
           //TempData is needed for Back button 
            TempData["Controller"] = "Income";
            TempData["Action"] = "CreateIncome";
            TempData["neededId"] = $"?clientId={clientId}";

            var clientModel = clientService.ClientInfo(clientId);
            var proceduresModel = procedureService.GetAllProcedures();

            var model = new WorkModel(clientModel, proceduresModel);
            
            return View(model);
        }
        
    }
}
