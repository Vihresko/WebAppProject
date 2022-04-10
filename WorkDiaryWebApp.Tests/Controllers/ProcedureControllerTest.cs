using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WorkDiaryWebApp.Controllers;
using WorkDiaryWebApp.Models.Procedure;
using WorkDiaryWebApp.Tests.Mocks;
using Xunit;

namespace WorkDiaryWebApp.Tests.Controllers
{
    public class ProcedureControllerTest
    {
        [Fact]
        public async Task Procedure_Must_Return_Correct_Result()
        {
            var procedureService = ProcedureServiceMock.Instance;
            var controller = new ProcedureController(procedureService, null);

            var result = await controller.Procedure("test");

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
