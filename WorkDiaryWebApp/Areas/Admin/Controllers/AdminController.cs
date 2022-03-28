using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.Models.Admin;

namespace WorkDiaryWebApp.Areas.Admin.Controllers
{
    public class AdminController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IAdminService adminService;

        public AdminController(RoleManager<IdentityRole> _roleManager, IAdminService _service)
        {
            roleManager = _roleManager;
            adminService = _service;
        }
        public async Task<IActionResult> CreateRole()
        {
            await roleManager.CreateAsync(new IdentityRole()
             {
                 Name = "Admin"
             }); 

            return Ok();
        }

        public IActionResult ManageUsers()
        {
            return View();
        }

        public async Task<IActionResult> ShowUsers()
        {
            var model = await adminService.GetUsersInfo();
            return View(model);
        }

        public async Task<IActionResult> EditUser(string userId)
        {
            var model = await adminService.GetUserInfo(userId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(ShowUserInfoModel model)
        {
           
            return Ok(model);
        }

    }
}
