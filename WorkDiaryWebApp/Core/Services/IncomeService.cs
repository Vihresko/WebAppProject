using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.Models.Income;
using WorkDiaryWebApp.WorkDiaryDB;
using WorkDiaryWebApp.WorkDiaryDB.Models;

namespace WorkDiaryWebApp.Core.Services
{
    public class IncomeService : IIncomeService
    {
        
        private readonly WorkDiaryDbContext database;
        public IncomeService(WorkDiaryDbContext _database )
        {
            database = _database;
        }
        public (bool, string) AddClientProcedureToVisitBag(AddIncomePostModel model, string userId)
        {
            var clientBagId = database.Clients.Where(c => c.Id == model.ClientId).Select(c => c.VisitBagId).FirstOrDefault();
            
            var work = new ClientProcedure()
            {
                UserId = userId,
                ProcedureId = model.ProcedureId,
                ClientId = model.ClientId,
                VisitBagId = clientBagId
            };

            database.ClientProcedures.Add(work);
            
            database.SaveChanges();
            return (true, null);
           
        }
    }
}
