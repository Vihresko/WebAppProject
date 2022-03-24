using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.Models.MainBank;
using WorkDiaryWebApp.WorkDiaryDB;

namespace WorkDiaryWebApp.Core.Services
{
    public class MainBankService : IMainBankService
    {
        private readonly WorkDiaryDbContext database;
        public MainBankService(WorkDiaryDbContext _database)
        {
            database = _database;
        }
        public MainBankInfoModel GetMainBankInfo()
        {
            var reports = database.Reports.OrderByDescending(r => r.DateTime).ToList();
            var balance = reports.Sum(r => r.Value);
            var model = new MainBankInfoModel()
            {
                Balance = balance,
                Reports = reports
            };

            return model;
        }
    }
}
