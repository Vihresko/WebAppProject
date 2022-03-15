using WorkDiaryWebApp.Models.Income;
using WorkDiaryWebApp.Models.Procedure;

namespace WorkDiaryWebApp.Core.Interfaces
{
    public interface IIncomeService
    {
        public (bool, string?) AddClientProcedureToVisitBag(AddIncomePostModel model, string clientId);

        public ListFromProcedures ShowClientHistory(string clientId);

        public ListFromProcedures ShowClientVisitBag(string clientId);

        public void RemoveProcedureFromVisitBag(string clientId, string procedureId);
    }
}
