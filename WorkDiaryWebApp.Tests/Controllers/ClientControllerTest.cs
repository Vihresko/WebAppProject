using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WorkDiaryWebApp.Controllers;
using WorkDiaryWebApp.Tests.Mocks;
using Xunit;

namespace WorkDiaryWebApp.Tests.Controllers
{
    public class ClientControllerTest
    {
        [Fact]
        public async Task Clients_Must_Return_ViewResult()
        {
            var clientService = ClientServiceMock.Instance;
            var controller = new ClientController(clientService);
            var result = await controller.Clients();
            
            Assert.NotNull(result);
            var resultData = Assert.IsType<ViewResult>(result);
            //TODO: Check model type and model View
            //Assert.IsAssignableFrom<ListFromClients>(resultData.Model);
        }

        [Fact]
        public async Task Client_Must_Return_ViewResult()
        {
            var clientService = ClientServiceMock.Instance;
            var controller = new ClientController(clientService);
            var result = await controller.Client("testId");

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AddClient_Get_Must_Return_ViewResult()
        {
            var clientService = ClientServiceMock.Instance;
            var controller = new ClientController(clientService);

            var result = controller.AddClient();
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task AddClient_Post_Must_Return_ViewResult()
        {
            var clientService = ClientServiceMock.Instance;
            var controller = new ClientController(clientService);
            var model = AddClientModelMock.Instance;

            var result = await controller.AddClient(model);

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task EditClient_Get_Must_Return_ViewResult()
        {
            var clientService = ClientServiceMock.Instance;
            var controller = new ClientController(clientService);

            var result = await controller.EditClient("test");
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task EditClient_Post_Must_Return_ViewResult()
        {
            var clientService = ClientServiceMock.Instance;
            var controller = new ClientController(clientService);
            var model = ClientInfoModelMock.Instance;

            var result = await controller.EditClient(model);

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

    }
}
