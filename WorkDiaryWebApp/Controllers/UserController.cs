using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly RoleManager<IdentityRole> roleManager;
        public UserController(IUserService _userService, RoleManager<IdentityRole> _roleManager)
        {
            userService = _userService;
            roleManager = _roleManager;
        }

        public async Task<IActionResult> CreateRole()
        {
           /* await roleManager.CreateAsync(new IdentityRole()
            {
                Name = "User"
            }); */

            return Ok();
        }

        [Authorize(Roles = UserConstant.Role.Administrator)]
        public async Task<IActionResult> ManageUsers()
        {
            return Ok();
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

            (bool isDone, StringBuilder errors) = await userService.RegisterNewUser(model);
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
