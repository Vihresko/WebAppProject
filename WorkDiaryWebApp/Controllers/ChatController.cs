using Microsoft.AspNetCore.Mvc;
using WorkDiaryWebApp.Models.Chat;

namespace WorkDiaryWebApp.Controllers
{
    public class ChatController : BaseControllerUser
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
