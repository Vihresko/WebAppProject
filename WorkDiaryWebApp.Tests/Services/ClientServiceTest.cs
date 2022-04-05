using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WorkDiaryWebApp.Constraints.Services;
using WorkDiaryWebApp.Models.Client;
using WorkDiaryWebApp.Tests.Mocks;
using Xunit;

namespace WorkDiaryWebApp.Tests.Services
{
    public class ClientServiceTest
    {
        private const string CREATE = "Create";
        private const string UPDATE = "Update";
        private const string DATE = "01/01/2000";
        private const string EMAIL = "create@abv.bg";
        private const string DATE_TO_STRING_FORMAT = "dd/MM/yyyy";
        private AddClientModel addClientModel = new AddClientModel()
        {
            FirstName = CREATE,
            LastName = CREATE,
            Email = EMAIL,
            BirthDay = DATE
        };

        [Fact]
        public async Task Add_Client_Must_Create_New_Client_In_Db()
        {
            using var data = DatabaseMock.Instance;
            var clientService = new ClientService(data);

            await clientService.AddNewClient(addClientModel);

            Assert.Equal(1, data.Clients.Count());
            var createdClient = data.Clients.FirstOrDefault();
            Assert.Equal(CREATE, createdClient?.FirstName);
            Assert.Equal(CREATE, createdClient?.LastName);
            Assert.Equal(EMAIL, createdClient?.Email);
            Assert.Equal(DATE, createdClient?.BirthDay.ToString(DATE_TO_STRING_FORMAT));
        }

        [Fact]
        public async Task Edit_Client_Must_Edit_Entity_In_Db()
        {
            using var data = DatabaseMock.Instance;
            var clientService = new ClientService(data);

            await clientService.AddNewClient(addClientModel);

            var clientId = data.Clients.First().Id;

            await clientService.EditClient(new ClientInfoModel
            {
                Id = clientId,
                FirstName = UPDATE,
                LastName = UPDATE,
                Email = "update@abv.bg",
                BirthDay = "10/10/1990"
            });

            var client = data.Clients.FirstOrDefault();
            Assert.NotEqual(CREATE, client?.FirstName);
            Assert.NotEqual(CREATE, client?.LastName);
            Assert.NotEqual(EMAIL, client?.Email);
            Assert.NotEqual(DATE, client?.BirthDay.ToString(DATE_TO_STRING_FORMAT));

            Assert.Equal(UPDATE, client?.FirstName);
        }

        [Fact]
        public async Task Client_Info_Must_Return_Client_With_Given_Id()
        {
            using var data = DatabaseMock.Instance;
            var clientService = new ClientService(data);

            await AddTwoClientsToDb(clientService);
            var clientId = await data.Clients.Where(c => c.FirstName == CREATE).Select(c => c.Id).FirstAsync();
            
            var client = await clientService.ClientInfo(clientId);

            Assert.IsType<ClientInfoModel>(client);
            Assert.Equal(clientId, client.Id);

        }

        [Fact]
        public async Task Get_All_Clients_Must_Return_All_Clients_From_Db()
        {
            using var data = DatabaseMock.Instance;
            var clientService = new ClientService(data);
            await AddTwoClientsToDb(clientService);

            var clients = await clientService.GetAllClients();

            Assert.IsType<ListFromClients>(clients);
            var count = clients.Clients.Count();
            Assert.Equal(2, count);
        }
        private async Task AddTwoClientsToDb(ClientService clientService)
        {
            await clientService.AddNewClient(addClientModel);
            await clientService.AddNewClient(new AddClientModel
            {
                FirstName = UPDATE,
                LastName = UPDATE,
                Email = "update@abv.bg",
                BirthDay = "15/10/1970"
            });
        }
    }
}
