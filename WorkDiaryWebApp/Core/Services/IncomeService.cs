using Microsoft.EntityFrameworkCore;
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
        public async Task<(bool, string?)> AddClientProcedureToVisitBag(AddIncomePostModel model, string userId)
        {
            var clientBagId = await database.Clients.Where(c => c.Id == model.ClientId).Select(c => c.VisitBagId).FirstOrDefaultAsync();
            
            var work = new ClientProcedure()
            {
                UserId = userId,
                ProcedureId = model.ProcedureId,
                ClientId = model.ClientId,
                VisitBagId = clientBagId
            };

            var isDoubling = await database.ClientProcedures.Where(cp => cp.VisitBagId == clientBagId && cp.ProcedureId == model.ProcedureId).AnyAsync();

            if (isDoubling)
            {
                return (false, "This procedure is already added in client visitbag!");
            }
            await database.ClientProcedures.AddAsync(work);
            await database.SaveChangesAsync();
            return (true, null);
           
        }
        public async Task<bool> CompleetePayment(PayPostModel model)
        {
            var userBank = await database.Users.Where(u => u.Id == model.UserId).Select(u => u.Bank).FirstOrDefaultAsync();
            var clientVisitBagId = await database.Clients.Where(c => c.Id == model.ClientId).Select(c => c.VisitBagId).FirstOrDefaultAsync();

            var income = new Income()
            {
                BankId = userBank.Id,
                Description = model.Description,
                Value = model.Value,
            };
            await database.Incomes.AddAsync(income);

            var clientProceduresForClose = await database.ClientProcedures.Where(cp => cp.UserId == model.UserId &&
                                                                                 cp.ClientId == model.ClientId &&
                                                                                 cp.VisitBagId == clientVisitBagId).ToListAsync();

            foreach (var cp in clientProceduresForClose)
            {
                cp.VisitBagId = null;
            }
            userBank.TakenMoney += model.Value;
            await database.SaveChangesAsync();
            return true;
        }

        public async Task<List<Income>> GetAllUsersIncomesHistory()
        {
            var usersIncomes = await database.Incomes.OrderByDescending(i => i.Id).ToListAsync();
            return usersIncomes;
        }

        public async Task<(string, decimal)> GetInfoForPayment(string clientId, decimal totalPrice, string username,ListFromProcedures procedures, decimal discount)
        {
            var document = new StringBuilder();
            var client = await database.Clients.Where(c => c.Id == clientId).FirstOrDefaultAsync();
            document.AppendLine($"Date: {DateTime.Now.ToString()}, '{client.FirstName} {client.LastName}' with email:'{client.Email}' pay below procedures to <{username}>:");
            int count = 0;
            foreach (var pr in procedures.Procedures)
            {
                count++;
                document.AppendLine($"{pr.Name}: -'{pr.Description}' price: {pr.Price}");
            }
            if (discount > 0)
            {
                totalPrice = totalPrice - (totalPrice * (discount / 100));
                document.AppendLine($"The client use discount from {discount}%!");
            }
            document.AppendLine($">>>Total price: {totalPrice:f2} {FormatConstant.CURRENCY}");
            return (document.ToString(), totalPrice);
        }

        public async Task<List<Income>> GetUnreportedUserIncomes(string userId)
        {
            var userBankId = await database.Users.Where(u => u.Id == userId).Select(u => u.BankId).FirstOrDefaultAsync();
            var userIncomes = await database.Incomes.Where(i => i.BankId == userBankId && i.IsReported == false).OrderByDescending(i => i.Id).ToListAsync();
            return userIncomes;
        }

        public async Task<List<Income>> GetUserIncomesHistory(string userId)
        {
            var userBankId = await database.Users.Where(u => u.Id == userId).Select(u => u.BankId).FirstOrDefaultAsync();
            var userIncomes = await database.Incomes.Where(i => i.BankId == userBankId && i.IsReported == true).OrderByDescending(i => i.Id).ToListAsync();
            return userIncomes;
        }

        public async Task RemoveProcedureFromVisitBag(string clientId, string procedureId)
        {
            var clientBagId = await database.Clients.Where(c => c.Id == clientId).Select(c => c.VisitBagId).FirstOrDefaultAsync();
            var workForDelete = await database.ClientProcedures.Where(cp => cp.ProcedureId == procedureId && cp.ClientId == clientId && cp.VisitBagId == clientBagId).FirstOrDefaultAsync();
            database.ClientProcedures.Remove(workForDelete);
            await database.SaveChangesAsync();
        }

        public async Task<bool> CleanUserDiary(string userId)
        {
            var userBankId = await database.Users.Where(u => u.Id == userId).Select(u => u.BankId).FirstOrDefaultAsync();
            var userIncomes = await database.Incomes.Where(i => i.BankId == userBankId && i.IsReported == false).ToListAsync();

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

        public async Task<ListFromProcedures> ShowClientHistory(string clientId)
        {
           var clientProceduresFromDb = await database.ClientProcedures.Where(cp => cp.ClientId == clientId && string.IsNullOrEmpty(cp.VisitBagId)).OrderByDescending(cp => cp.Date).ToListAsync();

            var model = new ListFromProcedures();
            foreach (var cp in clientProceduresFromDb)
            {
                var procedure = await database.Procedures.Where(p => p.Id == cp.ProcedureId).FirstOrDefaultAsync();
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

        public async Task<ListFromProcedures> ShowClientVisitBag(string clientId)
        {
            var clientProceduresFromDb = await database.ClientProcedures.Where(cp => cp.ClientId == clientId && cp.VisitBagId != null)
                                                                  .OrderByDescending(cp => cp.Date).ToListAsync();

            var model = new ListFromProcedures();
            foreach (var cp in clientProceduresFromDb)
            {
                var procedure = await database.Procedures.Where(p => p.Id == cp.ProcedureId).FirstOrDefaultAsync();
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
