﻿using Microsoft.AspNetCore.Mvc;
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
            var model = clientService.ClientInfo(clientId);
            return View(model);
        }

        public IActionResult AddClient()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddClient(AddClientModel model)
        {
            (bool isDone, string errors) = clientService.AddNewClient(model);

            if (isDone)
            {
                ViewData[MessageConstant.SuccessMessage] = "success";
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = errors;
            }
            return View();
        }


    }
}
