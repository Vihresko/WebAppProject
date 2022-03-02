using WorkDiaryCore.Models.Client;

namespace WorkDiaryCore.Constraints.Interfaces
{
    public interface IClientService
    {
        public (bool isDone, string errors) AddNewClient(object addClientModel);
    }
}
