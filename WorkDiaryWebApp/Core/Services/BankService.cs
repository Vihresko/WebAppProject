using Microsoft.EntityFrameworkCore;
using System.Text;
using WorkDiaryWebApp.Core.Constants;
using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.Models.Bank;
using WorkDiaryWebApp.WorkDiaryDB;
using WorkDiaryWebApp.WorkDiaryDB.Models;

namespace WorkDiaryWebApp.Core.Services
{
    public class BankService : IBankService
    {
        private readonly WorkDiaryDbContext database;
        public BankService(WorkDiaryDbContext _database)
        {
            database = _database;
        }
        public async Task <UserBankStatusGetModel> GetUserBankBalance(string userId)
        {
            var bank =  await GetUserBank(userId);
            var userBankStatus = new UserBankStatusGetModel()
            {
                UserId = userId,
                ReportedMoney = bank.ReportedMoney,
                TakenMoney = bank.TakenMoney
            };
            return userBankStatus;
        }
        public async Task<(bool, string)> ReportMoney(ReportMoneyPostModel model)
        {
            var bank = await GetUserBank(model.UserId);
            var neededMoney = bank.TakenMoney - bank.ReportedMoney;

            var message = new StringBuilder();
            var isOk = true;

            if(neededMoney > model.Value && model.Value > 0|| neededMoney == model.Value && model.Value > 0)
            {
                message.AppendLine("Done!");
            }
            else if(neededMoney < model.Value && model.Value > 0)
            {
                isOk = false;
                message.AppendLine($"You try to report 'MORE' than needed money:{neededMoney} {FormatConstant.CURRENCY}!");
            }
            else if(model.Value <=0)
            {
                isOk = false;
                message.AppendLine("Cannot report 'ZERO' or 'Negative' money!");
            }

            if (!isOk)
            {
                return (false, message.ToString());
            }
           
            bank.ReportedMoney+=model.Value;

            await AddReportToMainBank(model.UserId, model.Value);

            return (isOk, message.ToString());

        }
        private async Task AddReportToMainBank(string userId, decimal value)
        {
            var username = await database.Users.Where(u => u.Id == userId).Select(u => u.UserName).FirstAsync();
            var report = new Report()
            {
                Username = username,
                Value = value
            };
            var mainBank = await database.MainBanks.FirstAsync();
            mainBank.Reports.Add(report);
            mainBank.Balance += value;
            await database.SaveChangesAsync();
        }
        private async Task<Bank> GetUserBank(string userId)
        {
            var userBankId = await database.Users.Where(u => u.Id == userId).Select(u => u.BankId).FirstAsync();
            var bank = await database.Banks.Where(b => b.Id == userBankId).FirstAsync();
            return bank;
        }
    }
}
