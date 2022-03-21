using Microsoft.AspNetCore.Mvc;
using WorkDiaryWebApp.Core.Constants;
using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.Models.Client;

namespace WorkDiaryWebApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService clientService;
        public ClientController(IClientService _clientService)
        {
            clientService = _clientService;
        }
        public IActionResult Clients()
        {
            var model = clientService.GetAllClients();
            return View(model);
        }

       
        public IActionResult Client(string clientId)
        {
            var clientModel = clientService.ClientInfo(clientId);
            return View(clientModel);
        }

        public IActionResult AddClient()
        {
            return View(new AddClientModel());
        }
        [HttpPost]
        public IActionResult AddClient(AddClientModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            (bool isDone, string? errors) = clientService.AddNewClient(model);

            if (!isDone)
            {
                var splitedErrors = errors.ToString().Split(Environment.NewLine);

                foreach (var error in splitedErrors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }

            if (isDone)
            {
                ViewData[MessageConstant.SuccessMessage] = "Success!";
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = "Invalid data!";
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult EditClient(ClientInfoModel model)
        {
            (bool isDone, string? errors) = clientService.EditClient(model);

            if (isDone)
            {
                ViewData[MessageConstant.SuccessMessage] = CommonMessage.SUCCESS_MESSAGE;
            }
            else if (errors == CommonMessage.NO_CHANGES_MESSAGE)
            {
                ViewData[MessageConstant.WarningMessage] = CommonMessage.NO_CHANGES_MESSAGE;
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = errors;
            }
            return View(model);
        }

        public IActionResult EditClient(string clientId)
        {
            var model = clientService.ClientInfo(clientId);
            return View(model);
        }
    }
}
