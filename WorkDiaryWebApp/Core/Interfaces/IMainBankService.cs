using WorkDiaryWebApp.Models.MainBank;

namespace WorkDiaryWebApp.Core.Interfaces
{
    public interface IMainBankService
    {
        public Task<MainBankInfoModel> GetMainBankInfo();
    }
}
