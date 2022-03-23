using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkDiaryWebApp.Core.Interfaces;

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

        public async Task<IActionResult> ManageUsers()
        {
            return View();
        }

        public async Task<IActionResult> ShowUsers()
        {
            var model = adminService.GetUsersInfo();
            return View(model);
        }


    }
}
