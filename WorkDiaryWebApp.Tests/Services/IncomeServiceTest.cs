using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WorkDiaryWebApp.Core.Constants;
using WorkDiaryWebApp.Core.Services;
using WorkDiaryWebApp.Models.Income;
using WorkDiaryWebApp.Tests.Mocks;
using WorkDiaryWebApp.WorkDiaryDB.Models;
using Xunit;

namespace WorkDiaryWebApp.Tests.Services
{
    public class IncomeServiceTest
    {
        private const string userId1 = "userId1";
        private const string procedureId1 ="procedureId1";
        private const string clientId1 = "clientId1";
        private const string visitBagId1 = "visitBagId1";

        [Fact]
        public async Task Add_ClientProcedure_To_VisitBag_Must_Add_New_ClientProcedure_In_Db_If_Double_Return_Error()
        {
            using var data = DatabaseMock.Instance;
            var incomeService = new IncomeService(data);

            var income = new AddIncomePostModel()
            {
                ClientId = clientId1,
                ProcedureId = procedureId1
            };

            var result = await incomeService.AddClientProcedureToVisitBag(income, userId1);

            Assert.Equal(result, (true, null));
            Assert.Equal(1, await data.ClientProcedures.CountAsync());

            var clientProcedure = await data.ClientProcedures.FirstAsync();
            Assert.Equal(userId1, clientProcedure.UserId);
            Assert.Equal(procedureId1, clientProcedure.ProcedureId);
            Assert.Equal(clientId1, clientProcedure.ClientId);

            var error = await incomeService.AddClientProcedureToVisitBag(income, userId1);
            Assert.Equal((false, "This procedure is already added in client visitbag!"), error);
        }

        [Fact]
        public async Task CompleetePayment_Must_Add_New_Income_entity_In_Db_And_Raise_Bank_Taken_Money()
        {
            using var data = DatabaseMock.Instance;
            var incomeService = new IncomeService(data);

            var user = new User()
            {
                UserName = "test",
                Bank = new Bank(),
                ContactId = "contact",
                FullName = "Test Testov"
            };
            await data.Users.AddAsync(user);
            await data.SaveChangesAsync();

            var payPostModel = new PayPostModel()
            {
                ClientId = clientId1,
                Description = "test",
                UserId = user.Id,
                Value = 1,
                VisitBagId = visitBagId1
            };

            var result = await incomeService.CompleetePayment(payPostModel);
            var income = await data.Incomes.FirstOrDefaultAsync();

            Assert.NotNull(income);
            Assert.Equal("test", income.Description);
            Assert.Equal(1, income.Value);
            Assert.Equal(1, user.Bank.TakenMoney);

        }

        [Fact]
        public async Task GetAllUsersIncomesHistory_Must_Return_All_Incomes()
        {
            using var data = DatabaseMock.Instance;
            var incomeService = new IncomeService(data);

            var income1 = new Income()
            {
                BankId = "bank1",
                Value = 1,
                Description = "test"
            };
            var income2 = new Income()
            {
                BankId = "bank2",
                Value = 2,
                Description = "test"
            };
            await data.Incomes.AddRangeAsync(new Income[] { income1, income2 });
            await data.SaveChangesAsync();

            var count = await incomeService.GetAllUsersIncomesHistory();
            Assert.Equal(2, count.Count);
        }

        private Client CreateClient()
        {
            var client = new Client()
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "test@abv.bg",
                Id = clientId1,
                BirthDay = DateTime.ParseExact("01/01/2001", FormatConstant.DATE_TIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None),
                VisitBagId = visitBagId1
            };

            return client;
        }
    }
}
