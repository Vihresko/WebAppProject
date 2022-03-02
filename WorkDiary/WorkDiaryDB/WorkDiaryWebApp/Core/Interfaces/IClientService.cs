using WorkDiaryWebApp.Models.Client;

namespace WorkDiaryWebApp.Core.Interfaces
{
    public interface IClientService
    {
        public (bool isDone, string errors) AddNewClient(AddClientModel addClientModel);
        public ListFromClients GetAllClients();
    }
}
