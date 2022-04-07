using Microsoft.AspNetCore.Authorization;
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
            var controller = new ProcedureController(procedureService);

            var result = await controller.Procedure("test");

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
        
        //TODO: In controller have roles logic. How to pass it?
        [Fact]

        public async Task Procedures_Must_Return_Correct_Result()
        {
            var procedureService = ProcedureServiceMock.Instance;
            var controller = new ProcedureController(procedureService);

            var result = await controller.Procedures();
            
            Assert.NotNull(result);
            var resultData = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ListFromProcedures>(resultData.Model);
        }
    }
}
