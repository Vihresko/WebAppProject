using WorkDiaryWebApp.Models.Income;
using WorkDiaryWebApp.Models.Procedure;
using WorkDiaryWebApp.WorkDiaryDB.Models;

namespace WorkDiaryWebApp.Core.Interfaces
{
    public interface IIncomeService
    {
        public Task<(bool, string?)> AddClientProcedureToVisitBag(AddIncomePostModel model, string clientId);

        public Task<ListFromProcedures> ShowClientHistory(string clientId);

        public Task<ListFromProcedures> ShowClientVisitBag(string clientId);

        public Task RemoveProcedureFromVisitBag(string clientId, string procedureId);

        public Task<(string, decimal)> GetInfoForPayment(string clientId, decimal totalPrice, string username,ListFromProcedures procedures, decimal discount);

        public Task<bool> CompleetePayment(PayPostModel model);

        public Task<List<Income>> GetUserIncomesHistory(string userId);

        public Task<bool> CleanUserDiary(string userId);

        public Task<List<Income>> GetUnreportedUserIncomes(string userId);

        public Task<List<Income>> GetAllUsersIncomesHistory();
    }
}
