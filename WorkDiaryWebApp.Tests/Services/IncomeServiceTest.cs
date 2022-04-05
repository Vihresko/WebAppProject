using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WorkDiaryWebApp.Core.Constants;
using WorkDiaryWebApp.Core.Services;
using WorkDiaryWebApp.Models.Income;
using WorkDiaryWebApp.Tests.Mocks;
using WorkDiaryWebApp.WorkDiaryDB;
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

        [Fact]

        public async Task GetUnreportedUserIncomes_Must_Return_Only_Ureported_Incomes_On_User()
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

            var income1 = new Income()
            {
                BankId = user.Bank.Id,
                Value = 1,
                Description = "test",
                IsReported = false,
                
            };
            var income2 = new Income()
            {
                BankId = user.Bank.Id,
                Value = 2,
                Description = "test",
                IsReported = true
            };
            await data.Incomes.AddRangeAsync(new Income[] { income1, income2 });
            await data.SaveChangesAsync();

            var result = await incomeService.GetUnreportedUserIncomes(user.Id);
            int count = result.Count();
            Assert.Equal(1, count);


        }

        [Fact]

        public async Task GetUserIncomesHistory_Must_Return_Only_Selected_User_Incomes_Which_Are_Reported()
        {
            using var data = DatabaseMock.Instance;
            var incomeService = new IncomeService(data);

            var user1 = new User()
            {
                UserName = "test1",
                Bank = new Bank(),
                ContactId = "contact1",
                FullName = "Test Testov True"
            };

            var user2 = new User()
            {
                UserName = "test2",
                Bank = new Bank(),
                ContactId = "contact2",
                FullName = "Test Testov False"
            };
            await data.Users.AddRangeAsync(new User[] { user1, user2 });
            await data.SaveChangesAsync();

            var income1 = new Income()
            {
                BankId = user1.Bank.Id,
                Value = 1,
                Description = "test",
                IsReported = false,

            };
            var income2 = new Income()
            {
                BankId = user1.Bank.Id,
                Value = 2,
                Description = "test",
                IsReported = true
            };

            var income3 = new Income()
            {
                BankId = user2.Bank.Id,
                Value = 1,
                Description = "test",
                IsReported = false,

            };
            await data.Incomes.AddRangeAsync(new Income[] { income1, income2 });
            await data.SaveChangesAsync();

            var result = await incomeService.GetUserIncomesHistory(user1.Id);
            int count = result.Count();
            Assert.Equal(1, count);
            Assert.Equal(2, result.First().Value);
        }

        [Fact]
        public async Task CleanUserDiary_Must_Set_All_User_Incomes_To_Reported()
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

            var income = new Income()
            {
                BankId = user.Bank.Id,
                Value = 1,
                Description = "test",
                IsReported = false,
            };
            await data.Incomes.AddAsync(income);
            await data.SaveChangesAsync();

            await incomeService.CleanUserDiary(user.Id);
            var incomeFromDb = await data.Incomes.FirstOrDefaultAsync();
            Assert.NotNull(incomeFromDb);
            Assert.True(incomeFromDb?.IsReported == true);
        }

        [Fact]
        public async Task ShowClientHistory_Must_Return_All_Procedures_For_Selected_Client_With_Compleete_Payments()
        {
            using var data = DatabaseMock.Instance;
            var incomeService = new IncomeService(data);
            await CreateTwoClientProcedures(data);
           

            var result = await incomeService.ShowClientHistory(clientId1);
            int count = result.Procedures.Count;
            Assert.Equal(1, count);
            Assert.Equal("compleeted", result.Procedures.First().Name);
        }

        [Fact]
        public async Task ShowClientVisitBag_Must_Return_All_Procedures_On_Selected_Client_With_Not_Compleete_Payments()
        {
            using var data = DatabaseMock.Instance;
            var incomeService = new IncomeService(data);
            await CreateTwoClientProcedures(data);


            var result = await incomeService.ShowClientVisitBag(clientId1);
            int count = result.Procedures.Count;
            Assert.Equal(1, count);
            Assert.Equal("notCompleeted", result.Procedures.First().Name);

        }

        [Fact]
        public async Task RemoveProcedureFromVisitBag_Must_Remove_ClientProcedure_From_Given_Client()
        {
            using var data = DatabaseMock.Instance;
            var incomeService = new IncomeService(data);
            await CreateTwoClientProcedures(data);
            var procedureId = data.Procedures.First().Id;

            int countBefore = await data.ClientProcedures.CountAsync();
            Assert.Equal(2, countBefore);
            await incomeService.RemoveProcedureFromVisitBag(clientId1, procedureId);
            int countAfter = await data.ClientProcedures.CountAsync();
            Assert.Equal(1, countAfter);

        }

        private async Task CreateTwoClientProcedures(WorkDiaryDbContext data)
        {
            var client = CreateClient();

            var user = new User()
            {
                UserName = "test",
                Bank = new Bank(),
                ContactId = "contact",
                FullName = "Test Testov"
            };
            await data.Users.AddAsync(user);

            var procedure1 = new Procedure()
            {
                Name = "compleeted",
                Description = "test",
                Price = 10
            };

            var procedure2 = new Procedure()
            {
                Name = "notCompleeted",
                Description = "test",
                Price = 10
            };
            await data.Procedures.AddRangeAsync(new Procedure[] { procedure1, procedure2 });

            var clientProcedureCompleeted = new ClientProcedure()
            {
                ClientId = client.Id,
                ProcedureId = procedure1.Id,
                UserId = user.Id,
                VisitBagId = null,

            };

            var clientProcedureUncompleeted = new ClientProcedure()
            {
                ClientId = client.Id,
                ProcedureId = procedure2.Id,
                UserId = user.Id,
                VisitBagId = client.VisitBagId,

            };
            await data.ClientProcedures.AddRangeAsync(new ClientProcedure[] { clientProcedureCompleeted, clientProcedureUncompleeted });
            await data.SaveChangesAsync();
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
