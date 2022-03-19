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

        public UserBankStatusGetModel GetUserBankBalance(string userId)
        {
            var bank = GetUserBank(userId);
            var userBankStatus = new UserBankStatusGetModel()
            {
                UserId = userId,
                ReportedMoney = bank.ReportedMoney,
                TakenMoney = bank.TakenMoney
            };
            return userBankStatus;
        }

        public (bool, string) ReportMoney(ReportMoneyPostModel model)
        {
            var bank = GetUserBank(model.UserId);
            var neededMoney = bank.TakenMoney - bank.ReportedMoney;

            var message = new StringBuilder();
            var isOk = true;
            if(neededMoney > model.Value || neededMoney == model.Value && model.Value > 0)
            {
                message.AppendLine("Done!");
            }
            else if(neededMoney < model.Value)
            {
                isOk = false;
                message.AppendLine($"You try to report 'MORE' than needed money:{neededMoney} {FormatConstant.CURRENCY}!");
            }
            else if(neededMoney == model.Value)
            {
                isOk = false;
                message.AppendLine("Cannot report 'ZERO' money!");
            }

            bank.ReportedMoney+=model.Value;
            database.SaveChanges();
            return (isOk, message.ToString());

        }

        private Bank GetUserBank(string userId)
        {
            var userBankId = database.Users.Where(u => u.Id == userId).Select(u => u.BankId).First();
            var bank = database.Banks.Where(b => b.Id == userBankId).First();
            return bank;
        }
    }
}
