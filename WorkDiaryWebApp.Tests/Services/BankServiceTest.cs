using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WorkDiaryWebApp.Core.Services;
using WorkDiaryWebApp.Models.Bank;
using WorkDiaryWebApp.Tests.Mocks;
using WorkDiaryWebApp.WorkDiaryDB;
using WorkDiaryWebApp.WorkDiaryDB.Models;
using Xunit;

namespace WorkDiaryWebApp.Tests.Services
{
    public class BankServiceTest
    {
        private const decimal takenMoney = 10;
        private const decimal reportedMoney = 5;
        private const decimal moneyValueForReport = 2;
        [Fact]
        public async Task GetUserBankBalance_Must_Return_User_Bank_Information()
        {
            using var data = DatabaseMock.Instance;
            var incomeService = new BankService(data);

            var userId = await CreateUserAndBank(data);

            var result = await incomeService.GetUserBankBalance(userId);
            Assert.NotNull(result);
            Assert.Equal(takenMoney, result.TakenMoney);
            Assert.Equal(reportedMoney, result.ReportedMoney);
        }

        [Fact]
        public async Task ReportMoney_Must_Actualize_User_Bank_Status_And_Main_Bank()
        {
            using var data = DatabaseMock.Instance;
            var incomeService = new BankService(data);

            var userId = await CreateUserAndBank(data);

            var reportMoneyPostmodel = new ReportMoneyPostModel()
            {
                UserId = userId,
                Value = moneyValueForReport
            };

            await data.MainBanks.AddAsync(new MainBank());
            await data.SaveChangesAsync();

            var result = await incomeService.ReportMoney(reportMoneyPostmodel);

            var userBank = await data.Banks.FirstOrDefaultAsync();
            Assert.NotNull(userBank);
            Assert.Equal(7, userBank?.ReportedMoney);
            var mainBank = await data.MainBanks.FirstAsync();
            Assert.Equal(moneyValueForReport, mainBank.Balance);

        }

        private async Task<string> CreateUserAndBank(WorkDiaryDbContext data)
        {
            var user = new User()
            {
                UserName = "test",
                Bank = new Bank(),
                ContactId = "contact",
                FullName = "Test Testov"
            };
            user.Bank.TakenMoney = takenMoney;
            user.Bank.ReportedMoney = reportedMoney;

            await data.Users.AddAsync(user);
            await data.SaveChangesAsync();
            return user.Id;
        }
    }
}
