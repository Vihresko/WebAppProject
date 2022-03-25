using Microsoft.AspNetCore.Mvc;
using WorkDiaryWebApp.Core.Interfaces;

namespace WorkDiaryWebApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService contactService;
        public ContactController(IContactService _contactService)
        {
            contactService = _contactService;
        }
        public async Task<IActionResult> Contacts()
        {
            var model = await contactService.GetAllContacts();

            return View(model);
        }
    }
}
