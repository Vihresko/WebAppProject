using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WorkDiaryWebApp.Core.Interfaces;

namespace WorkDiaryWebApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService contactService;
        private readonly IMemoryCache memoryCash;

        public ContactController(IContactService _contactService, IMemoryCache _memoryCash)
        {
            contactService = _contactService;
            memoryCash = _memoryCash;
        }
        public async Task<IActionResult> Contacts()
        {
            if (!memoryCash.TryGetValue("Contacts", out var model))
            {
                model = await contactService.GetAllContacts();
                memoryCash.Set("Contacts", model, TimeSpan.FromSeconds(10));
            }
            return View(model);
        }
    }
}
