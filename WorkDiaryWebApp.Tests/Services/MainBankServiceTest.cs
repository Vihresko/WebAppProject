using System.Threading.Tasks;
using WorkDiaryWebApp.Core.Services;
using WorkDiaryWebApp.Tests.Mocks;
using WorkDiaryWebApp.WorkDiaryDB.Models;
using Xunit;

namespace WorkDiaryWebApp.Tests.Services
{
    public class MainBankServiceTest
    {
        [Fact]
        public async Task GetMainBankInfo_Must_Return_Main_Bank_Status()
        {
            using var data = DatabaseMock.Instance;
            var mainBankService = new MainBankService(data);

            var report1 = new Report()
            {
                Value = 10,
                Username = "test"
            };

            var report2 = new Report()
            {
                Value = 20,
                Username = "test"
            };

            await data.Reports.AddRangeAsync(new Report[] { report1, report2 });
            await data.SaveChangesAsync();

            var result = await mainBankService.GetMainBankInfo();
            Assert.NotNull(result);
            Assert.Equal(30, result.Balance);
        }
    }
}
