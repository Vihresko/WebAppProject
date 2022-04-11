using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkDiaryWebApp.Core.Services;
using WorkDiaryWebApp.Models.Outcome;
using WorkDiaryWebApp.Tests.Mocks;
using WorkDiaryWebApp.WorkDiaryDB.Models;
using Xunit;

namespace WorkDiaryWebApp.Tests.Services
{
    public class OutcomeServiceTest
    {
        [Fact]
        public async Task AddOutcome_Must_Add_Entity_To_Db()
        {
            using var data = DatabaseMock.Instance;
            var outcomeService = new OutcomeService(data);
            var mainBank = new MainBank()
            {
                Balance = 10
            };
            await data.MainBanks.AddAsync(mainBank);
            await data.SaveChangesAsync();
            var outcome = new AddOutcomeModel()
            {
                Description = "test",
                Value = 1
            };

            var result = await outcomeService.AddOutcome(outcome);
            Assert.Equal((true, ""), result);
            Assert.Equal(1, await data.Outcomes.CountAsync());
            var outcomeFromDb = await data.Outcomes.FirstAsync();
            Assert.Equal("test", outcomeFromDb.Description);
            Assert.Equal(1, outcomeFromDb.Value);
        }

        [Fact]
        public async Task GetAllOutComes_Must_Return_All_Outcomes_From_Db()
        {
            using var data = DatabaseMock.Instance;
            var outcomeService = new OutcomeService(data);
            var mainBank = new MainBank()
            {
                Balance = 10
            };
            await data.MainBanks.AddAsync(mainBank);
            await data.SaveChangesAsync();
            var outcome1 = new Outcome()
            {
                Description = "test",
                Value = 1
            };
            var outcome2 = new Outcome()
            {
                Description = "test",
                Value = 1
            };

            await data.Outcomes.AddRangeAsync(new Outcome[] { outcome1, outcome2 });
            await data.SaveChangesAsync();  

            var result = await outcomeService.GetAllOutcomes();
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

        }

    }
}
