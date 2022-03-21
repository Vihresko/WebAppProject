using System.Text;
using WorkDiaryWebApp.Core.Constants;
using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.Models.Procedure;
using WorkDiaryWebApp.WorkDiaryDB;
using WorkDiaryWebApp.WorkDiaryDB.Models;

namespace WorkDiaryWebApp.Core.Services
{
    using static WorkDiaryWebApp.WorkDiaryDB.Constraints.Constants;

    public class ProcedureService : IProcedureService
    {
        private readonly WorkDiaryDbContext database;
        public ProcedureService(WorkDiaryDbContext db)
        {
            database = db;
        }
        public (bool, string?) AddNewProcedure(AddProcedureModel model)
        {
            
            (bool isValidModel, string errors) = ValidateProcedureValues(model.Name, model.Description, model.Price);

            if (!isValidModel)
            {
                return (isValidModel, errors.ToString());
            }

            var isExist = database.Procedures.Any(p => p.Name == model.Name && p.Description == model.Description);

            if (isExist)
            {
                return (false, MessageConstant.DoubleEntity);
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

        public (bool, string?) EditProcedure(ShowProcedureModel model)
        {
            (bool isValidModel, string errors) = ValidateProcedureValues(model.Name, model.Description, model.Price);

            if (!isValidModel)
            {
                return (isValidModel, errors.ToString());
            }
            var originState = database.Procedures.Where(p => p.Id == model.Id).FirstOrDefault();

            if (originState.Name == model.Name &&
               originState.Description == model.Description &&
               originState.Price == model.Price &&
               originState.IsActive == model.IsActive)
            {
                return (false, CommonMessage.NO_CHANGES_MESSAGE);
            }

            try
            {
                originState.Name = model.Name;
                originState.Description = model.Description;
                originState.Price = model.Price;
                originState.IsActive = model.IsActive;
                database.SaveChanges();
            }
            catch
            {
                return (false, "Fail to edit procedure from database");
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
                    Description = procedure.Description,
                    Price = procedure.Price,
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
                Price = procedureFromDb.Price,
                Id = procedureFromDb.Id,
                IsActive = procedureFromDb.IsActive
            };
            return model;
        }

        private (bool, string) ValidateProcedureValues(string Name, string? Description, decimal Price)
        {
            bool isValidModel = true;
            var errors = new StringBuilder();

            if (Name == null || Name.Length < PROCEDURE_NAME_MIN_LENGTH || Name.Length > PROCEDURE_NAME_MAX_LENGTH)
            {
                isValidModel = false;
                errors.AppendLine($"{nameof(Name)} must be between {PROCEDURE_NAME_MIN_LENGTH} and {PROCEDURE_NAME_MAX_LENGTH} characters!");
            }

            if (Description != null && Description.Length > PROCEDURE_DESCRIPTION_MAX_LENGTH)
            {
                isValidModel = false;
                errors.AppendLine($"{nameof(Description)} must be maximum from {PROCEDURE_DESCRIPTION_MAX_LENGTH} symbols!");
            }
            if (Price < PROCEDURE_MIN_PRICE || Price > decimal.MaxValue)
            {
                isValidModel = false;
                errors.AppendLine($"{nameof(Price)} must be between {PROCEDURE_MIN_PRICE} and {PROCEDURE_MAX_PRICE}");
            }

            return (isValidModel, errors.ToString());
        }
    }
}
