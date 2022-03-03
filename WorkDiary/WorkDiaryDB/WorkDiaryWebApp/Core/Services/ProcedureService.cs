using System.Text;
using WorkDiaryDB;
using WorkDiaryDB.Models;
using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.Models.Procedure;

namespace WorkDiaryWebApp.Core.Services
{
    using static WorkDiaryDB.Constraints.Constants;

    public class ProcedureService : IProcedureService
    {
        private readonly WorkDiaryDbContext database;
        public ProcedureService(WorkDiaryDbContext db)
        {
            database = db;
        }
        public (bool isDone, string errors) AddNewProcedure(AddProcedureModel model)
        {
            bool isValidModel = true;
            var errors = new StringBuilder();

            if(model.Name == null || model.Name.Length < PROCEDURE_NAME_MIN_LENGTH || model.Name.Length > PROCEDURE_NAME_MAX_LENGTH)
            {
                isValidModel = false;
                errors.AppendLine($"{nameof(model.Name)} must be between {PROCEDURE_NAME_MIN_LENGTH} and {PROCEDURE_NAME_MAX_LENGTH} characters!");
            }

            if(model.Description != null && model.Description.Length > PROCEDURE_DESCRIPTION_MAX_LENGTH)
            {
                isValidModel = false;
                errors.AppendLine($"{nameof(model.Description)} must be maximum from {PROCEDURE_DESCRIPTION_MAX_LENGTH} symbols!");
            }
            if(model.Price < PROCEDURE_MIN_PRICE || model.Price > decimal.MaxValue)
            {
                isValidModel = false;
                errors.AppendLine($"{nameof(model.Price)} must be between {PROCEDURE_MIN_PRICE} and {PROCEDURE_MAX_PRICE}");
            }

            if (!isValidModel)
            {
                return (isValidModel, errors.ToString());
            }

            Procedure newProcedure = new Procedure()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price
            };

            try
            {
                database.Procedures.Add(newProcedure);
                database.SaveChanges();
            }
            catch
            {
                return (false, "Fail to add procedure to database");
            }
            return (true, null);
        }

        public ListFromProcedures GetAllProcedures()
        {
            var model = new ListFromProcedures();
            var proceduresFromDB = database.Procedures.ToHashSet();
            foreach (var procedure in proceduresFromDB)
            {
                ShowProcedureModel procedureModel = new ShowProcedureModel()
                {
                    Name = procedure.Name,
                    Description =procedure.Description,
                    Price =procedure.Price,
                    Id = procedure.Id
                };
                model.Procedures.Add(procedureModel);
            }
            model.Procedures = model.Procedures.OrderBy(c => c.Name).ToHashSet();
            return model;
        }

        public ShowProcedureModel ProcedureInfo(string procedureId)
        {
            var procedureFromDb = database.Procedures.Where(p => p.Id == procedureId).FirstOrDefault();
            ShowProcedureModel model = new ShowProcedureModel()
            {
                 Name = procedureFromDb.Name,
                 Description = procedureFromDb.Description,
                 Price =procedureFromDb.Price,
                 Id =procedureFromDb.Id
            };
            return model;
        }
    }
}
