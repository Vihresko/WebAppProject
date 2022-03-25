using WorkDiaryWebApp.Models.Bank;

namespace WorkDiaryWebApp.Core.Interfaces
{
    public interface IBankService
    {
        public Task<(bool, string)> ReportMoney(ReportMoneyPostModel model);

        public Task<UserBankStatusGetModel> GetUserBankBalance(string userId);
    }
}
