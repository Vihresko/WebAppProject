using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WorkDiaryWebApp.Areas.Admin.Controllers;
using WorkDiaryWebApp.Tests.Mocks;
using Xunit;

namespace WorkDiaryWebApp.Tests.Controllers
{
    public class ProcedureControllerAdminTest
    {
        [Fact]
        public async Task AddProcedure_Must_Return_Correct_Result()
        {
            var procedureService = ProcedureServiceMock.Instance;
            var controller = new ProcedureController(procedureService);

            var result = await controller.AddProcedure(AddProcedureModelMock.Instance);

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public async Task EditProcedure_Get_Must_Return_Correct_Result()
        {
            var procedureService = ProcedureServiceMock.Instance;
            var controller = new ProcedureController(procedureService);

            var result = await controller.EditProcedure("test");

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public async Task EditProcedure_Post_Must_Return_Correct_Result()
        {
            var procedureService = ProcedureServiceMock.Instance;
            var controller = new ProcedureController(procedureService);

            var result = await controller.EditProcedure(ShowProcedureModelMock.Instance);

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
