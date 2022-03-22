using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WorkDiaryWebApp.Areas.Admin.Controllers
{
    public class AdminController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController(RoleManager<IdentityRole> _roleManager)
        {
            roleManager = _roleManager;
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
            return Ok();
        }
    }
}
