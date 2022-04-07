using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WorkDiaryWebApp.Controllers;
using WorkDiaryWebApp.Tests.Mocks;
using Xunit;

namespace WorkDiaryWebApp.Tests.Controllers
{
    public class BankControllerTest
    {
        [Fact] public async Task UserBank_Must_Return_Correct_Result()
        {
            var bankService = BankServiceMock.Instance;
            var controller = new BankController(bankService, null);

            var result = await controller.UserBank();
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
