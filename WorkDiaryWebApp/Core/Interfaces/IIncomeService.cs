using WorkDiaryWebApp.Models.Income;
using WorkDiaryWebApp.Models.Procedure;
using WorkDiaryWebApp.WorkDiaryDB.Models;

namespace WorkDiaryWebApp.Core.Interfaces
{
    public interface IIncomeService
    {
        public (bool, string?) AddClientProcedureToVisitBag(AddIncomePostModel model, string clientId);

        public ListFromProcedures ShowClientHistory(string clientId);

        public ListFromProcedures ShowClientVisitBag(string clientId);

        public void RemoveProcedureFromVisitBag(string clientId, string procedureId);

        public string GetInfoForPayment(string clientId, decimal totalPrice, string userId, ListFromProcedures procedures);

        public bool CompleetePayment(PayPostModel model);

        public List<Income> GetUserIncomesHistory(string userId);

        public Task<bool> ReportAllUserIncomes(string userId);

        public List<Income> GetUnreportedUserIncomes(string userId);
    }
}
