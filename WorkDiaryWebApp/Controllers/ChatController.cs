using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkDiaryWebApp.Core.Constants;
using WorkDiaryWebApp.Models.Chat;

namespace WorkDiaryWebApp.Controllers
{
    [Authorize(Roles = $"{UserConstant.Role.USER}, {UserConstant.Role.ADMINISTRATOR}")]
    public class ChatController : Controller
    {
        public IActionResult Chat()
        {
            var model = new ChatModel()
            {
                Username = User.Identity.Name
            };
            return View(model);
        }
    }
}
