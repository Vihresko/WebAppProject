using System.Text;
using WorkDiaryWebApp.Core.Constants;
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
        public (bool, string?) AddClientProcedureToVisitBag(AddIncomePostModel model, string userId)
        {
            var clientBagId = database.Clients.Where(c => c.Id == model.ClientId).Select(c => c.VisitBagId).FirstOrDefault();
            
            var work = new ClientProcedure()
            {
                UserId = userId,
                ProcedureId = model.ProcedureId,
                ClientId = model.ClientId,
                VisitBagId = clientBagId
            };

            var isDoubling = database.ClientProcedures.Where(cp => cp.VisitBagId == clientBagId && cp.ProcedureId == model.ProcedureId).Any();

            if (isDoubling)
            {
                return (false, "This procedure is already added in client visitbag!");
            }
            database.ClientProcedures.Add(work);
            database.SaveChanges();
            return (true, null);
           
        }

        public bool CompleetePayment(PayPostModel model)
        {
            var userBank = database.Users.Where(u => u.Id == model.UserId).Select(u => u.Bank).FirstOrDefault();
            var clientVisitBagId = database.Clients.Where(c => c.Id == model.ClientId).Select(c => c.VisitBagId).FirstOrDefault();

            var income = new Income()
            {
                BankId = userBank.Id,
                Description = model.Description,
                Value = model.Value,
            };
            database.Incomes.Add(income);

            var clientProceduresForClose = database.ClientProcedures.Where(cp => cp.UserId == model.UserId &&
                                                                                 cp.ClientId == model.ClientId &&
                                                                                 cp.VisitBagId == clientVisitBagId).ToList();

            foreach (var cp in clientProceduresForClose)
            {
                cp.VisitBagId = null;
            }
            userBank.TakenMoney += model.Value;
            database.SaveChanges();
            return true;
        }

        public List<Income> GetAllUsersIncomesHistory()
        {
            var usersIncomes = database.Incomes.Where(i => i.IsReported == true).OrderByDescending(i => i.Id).ToList();
            return usersIncomes;
        }

        public string GetInfoForPayment(string clientId, decimal totalPrice, string userId, ListFromProcedures procedures)
        {
            var document = new StringBuilder();
            var client = database.Clients.Where(c => c.Id == clientId).FirstOrDefault();
            document.AppendLine($"Today: {DateTime.Now.ToString()}, '{client.FirstName} {client.LastName}' with email:'{client.Email}' pay below procedures:");
            int count = 0;
            foreach (var pr in procedures.Procedures)
            {
                count++;
                document.AppendLine($"{pr.Name}: -'{pr.Description}' price: {pr.Price}");
            }
            //TODO: valuta
            document.AppendLine($">>>Total price: {totalPrice} {FormatConstant.CURRENCY}");
            //TODO: if have promotion? 
            return document.ToString();
        }

        public List<Income> GetUnreportedUserIncomes(string userId)
        {
            var userBankId = database.Users.Where(u => u.Id == userId).Select(u => u.BankId).FirstOrDefault();
            var userIncomes = database.Incomes.Where(i => i.BankId == userBankId && i.IsReported == false).OrderByDescending(i => i.Id).ToList();
            return userIncomes;
        }

        public List<Income> GetUserIncomesHistory(string userId)
        {
            var userBankId = database.Users.Where(u => u.Id == userId).Select(u => u.BankId).FirstOrDefault();
            var userIncomes = database.Incomes.Where(i => i.BankId == userBankId && i.IsReported == true).OrderByDescending(i => i.Id).ToList();
            return userIncomes;
        }


        public void RemoveProcedureFromVisitBag(string clientId, string procedureId)
        {
            var clientBagId = database.Clients.Where(c => c.Id == clientId).Select(c => c.VisitBagId).FirstOrDefault();
            var workForDelete = database.ClientProcedures.Where(cp => cp.ProcedureId == procedureId && cp.ClientId == clientId && cp.VisitBagId == clientBagId).FirstOrDefault();
            database.ClientProcedures.Remove(workForDelete);
            database.SaveChanges();
        }

        public async Task<bool> CleanUserDiary(string userId)
        {
            var userBankId = database.Users.Where(u => u.Id == userId).Select(u => u.BankId).FirstOrDefault();
            var userIncomes = database.Incomes.Where(i => i.BankId == userBankId && i.IsReported == false).ToList();

            foreach (var income in userIncomes)
            {
                income.IsReported = true;
            }

            try
            {
                await database.SaveChangesAsync();
            }
            catch (Exception)
            {
               return false;
            }
            
            return true;
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
