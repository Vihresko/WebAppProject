using WorkDiaryWebApp.Models.Income;

namespace WorkDiaryWebApp.Core.Interfaces
{
    public interface IIncomeService
    {
        public (bool, string) AddClientProcedureToVisitBag(AddIncomePostModel model, string clientId);
    }
}
