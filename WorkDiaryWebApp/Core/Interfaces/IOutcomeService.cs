using WorkDiaryWebApp.Models.Outcome;
using WorkDiaryWebApp.WorkDiaryDB.Models;

namespace WorkDiaryWebApp.Core.Interfaces
{
    public interface IOutcomeService
    {
        public Task<(bool, string)> AddOutcome(AddOutcomeModel model);
        public Task<List<Outcome>> GetAllOutcomes();
        public Task<decimal> GetMainBankVault();
    }
}
