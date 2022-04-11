using Microsoft.EntityFrameworkCore;
using System.Text;
using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.Models.Outcome;
using WorkDiaryWebApp.WorkDiaryDB;
using WorkDiaryWebApp.WorkDiaryDB.Models;

namespace WorkDiaryWebApp.Core.Services
{
    public class OutcomeService : IOutcomeService
    {
        private readonly WorkDiaryDbContext database;
        public OutcomeService(WorkDiaryDbContext _database)
        {
            database = _database;
        }
        public async Task<(bool, string)> AddOutcome(AddOutcomeModel model)
        {
            var errors = new StringBuilder();
            var result = true;
            var mainBank = await database.MainBanks.FirstOrDefaultAsync();
            var newOutcome = new Outcome()
            {
                Description = model.Description,
                Value = model.Value,
                MainBank = mainBank
            };
            var vaultMoneyValue = mainBank.Balance;

            if(vaultMoneyValue > 0 && vaultMoneyValue >= model.Value)
            {
                mainBank.Balance = vaultMoneyValue - model.Value;
                await database.Outcomes.AddAsync(newOutcome);
                await database.SaveChangesAsync();
            }
            else
            {
                errors.AppendLine("Not enough money for this payment!");
                result = false;
            }
            return(result, errors.ToString());
        }

        public async Task<List<Outcome>> GetAllOutcomes()
        {
            var model = await database.Outcomes.OrderByDescending(o => o.DateTime).ToListAsync();
            return model;
        }

        public async Task<decimal> GetMainBankVault()
        {
            var result = await database.MainBanks.Select(mb => mb.Balance).FirstOrDefaultAsync();
            return result;
        }
    }
}
