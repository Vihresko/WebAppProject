using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WorkDiaryWebApp.Controllers;
using WorkDiaryWebApp.Tests.Mocks;
using Xunit;

namespace WorkDiaryWebApp.Tests.Controllers
{
    public class ContactControllerTest
    {
        [Fact]
        public async Task Contacts_Must_Return_Corect_Result()
        {
            var contactService = ContactServiceMock.Instance;
            var controller = new ContactController(contactService);

            var result = await controller.Contacts();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
