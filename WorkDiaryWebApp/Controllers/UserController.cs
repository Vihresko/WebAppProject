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
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IUserService userService;
        public UserController(UserManager<User> _userManager, SignInManager<User> _signInManager, IUserService _userService)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            userService = _userService;
        }

        public IActionResult Register()
        {
            return View(new RegisterFormModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterFormModel model)
        {
            
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
                ViewData[MessageConstant.SuccessMessage] = "success";
            }
            else
            {
                ViewData[MessageConstant.ErrorMessage] = errors;
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
            bool isConfirm = await userService.TryToLogin(model);
            if (isConfirm)
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
