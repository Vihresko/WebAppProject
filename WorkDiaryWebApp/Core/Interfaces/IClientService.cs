using WorkDiaryWebApp.Models.Client;

namespace WorkDiaryWebApp.Core.Interfaces
{
    public interface IClientService
    {
        public Task<(bool, string?)> AddNewClient(AddClientModel addClientModel);
        public Task<ListFromClients> GetAllClients();

        public Task<ClientInfoModel> ClientInfo(string clientId);

        public Task<(bool, string?)> EditClient(ClientInfoModel model);
    }
}
