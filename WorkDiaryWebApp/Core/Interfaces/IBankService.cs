using WorkDiaryWebApp.Models.Bank;

namespace WorkDiaryWebApp.Core.Interfaces
{
    public interface IBankService
    {
        public (bool, string) ReportMoney(ReportMoneyPostModel model);

        public UserBankStatusGetModel GetUserBankBalance(string userId);
    }
}
