using Microsoft.EntityFrameworkCore;
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
        public async Task<MainBankInfoModel> GetMainBankInfo()
        {
            var reports = await database.Reports.OrderByDescending(r => r.DateTime).ToListAsync();
            var outcomes = await database.Outcomes.OrderByDescending(r => r.DateTime).ToListAsync();
            var balance = reports.Sum(r => r.Value) - outcomes.Sum(o => o.Value);
            var model = new MainBankInfoModel()
            {
                Balance = balance,
                Reports = reports
            };

            return model;
        }

    }
}
