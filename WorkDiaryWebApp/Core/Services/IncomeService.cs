using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.Models.Income;
using WorkDiaryWebApp.Models.Procedure;
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

        public ListFromProcedures ShowClientHistory(string clientId)
        {
           var clientProceduresFromDb = database.ClientProcedures.Where(cp => cp.ClientId == clientId && string.IsNullOrEmpty(cp.VisitBagId)).OrderByDescending(cp => cp.Date).ToList();

            var model = new ListFromProcedures();
            foreach (var cp in clientProceduresFromDb)
            {
                var procedure = database.Procedures.Where(p => p.Id == cp.ProcedureId).FirstOrDefault();
                ShowProcedureModel procedureModel = new ShowProcedureModel()
                {
                    Name = procedure.Name,
                    Description = procedure.Description,
                    Id = procedure.Id,
                    DateForHistory = cp.Date
                };
                model.Procedures.Add(procedureModel);
            }
           
            return model;
        }

        public ListFromProcedures ShowClientVisitBag(string clientId)
        {
            var clientProceduresFromDb = database.ClientProcedures.Where(cp => cp.ClientId == clientId && cp.VisitBagId != null)
                                                                  .OrderByDescending(cp => cp.Date).ToList();

            var model = new ListFromProcedures();
            foreach (var cp in clientProceduresFromDb)
            {
                var procedure = database.Procedures.Where(p => p.Id == cp.ProcedureId).FirstOrDefault();
                ShowProcedureModel procedureModel = new ShowProcedureModel()
                {
                    Name = procedure.Name,
                    Description = procedure.Description,
                    Id = procedure.Id,
                    DateForHistory = cp.Date,
                    Price = procedure.Price,
                };
                model.Procedures.Add(procedureModel);
            }

            return model;
        }
    }
}
