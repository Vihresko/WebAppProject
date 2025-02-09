﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WorkDiaryWebApp.Core.Constants;
using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.Models.User;
using WorkDiaryWebApp.WorkDiaryDB.Models;

namespace WorkDiaryWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IAdminService adminService;
        private readonly UserManager<User> userManager;
        public UserController(IUserService _userService, IAdminService _adminService, UserManager<User> _userManager)
        {
            userService = _userService;
            adminService = _adminService;
            userManager = _userManager;
        }

        public IActionResult Register()
        {
            return View(new RegisterFormModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            (bool isDone, StringBuilder errors, string userName) = await userService.RegisterNewUser(model);
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
                ViewData[MessageConstant.SuccessMessage] = CommonMessage.SUCCESS_MESSAGE;
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = CommonMessage.INVALID_DATA;
            }

            /*
            For easy tests:
            -First Registred user is set to Admin and User roles.
            -In the first registration MainBank is created 
            */
            var firstUser = await adminService.IsThatFirstRegistration();
            
            if(firstUser != null)
            {
                await adminService.CreateRolesAndMainBank();
                string[] roles = {UserConstant.Role.ADMINISTRATOR, UserConstant.Role.USER };
                var roleresult = await userManager.AddToRolesAsync(firstUser, roles);
            }
            else
            {
                var identityUser = userManager.Users.First(u => u.UserName == userName);
                var giveDefaultRole = await userManager.AddToRolesAsync(identityUser, new string[] { UserConstant.Role.GUEST });
            }

            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
            
            bool isSuccess = await userService.TryToLogin(model);
            if (isSuccess)
            {
                return Redirect("/Home/Index");
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = CommonMessage.NOT_REGISTRED_USER;
            }
            return View();
        }
    }
}
