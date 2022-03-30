using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;
using WorkDiaryWebApp.Core.Constants;
using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.Models.Client;
using WorkDiaryWebApp.WorkDiaryDB;
using WorkDiaryWebApp.WorkDiaryDB.Models;

namespace WorkDiaryWebApp.Constraints.Services
{
    using static WorkDiaryWebApp.WorkDiaryDB.Constraints.Constants;
    public class ClientService : IClientService
    {
        private readonly WorkDiaryDbContext database;
        public ClientService(WorkDiaryDbContext db)
        {
            database = db;
        }
        public async Task<(bool, string?)> AddNewClient(AddClientModel model)
        {
            (bool isValidModel, string errors)  = await ValidateClientModel(model.FirstName, model.LastName, model.Email, model.BirthDay);

            if (!isValidModel)
            {
                return (isValidModel, errors);
            }

            Client newClient = new Client()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                BirthDay = DateTime.ParseExact(model.BirthDay, FormatConstant.DATE_TIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None),
                VisitBag = new VisitBag()
            };

            var areExist = await database.Clients.AnyAsync(x => x.FirstName == model.FirstName && x.LastName == model.LastName && x.Email == model.Email);
            if (areExist)
            {
                return (false, "This user already exist!");
            }

            try
            {
                await database.Clients.AddAsync(newClient);
                await database.SaveChangesAsync();
            }
            catch
            {
                return (false, CommonMessage.DATABASE_ERROR);
            }
            return (true, null);
        }

        public async Task<ClientInfoModel> ClientInfo(string clientId)
        {
            var clientFromDb = await database.Clients.Where(c => c.Id == clientId).FirstOrDefaultAsync();
            ClientInfoModel model = new ClientInfoModel()
            {
                FirstName = clientFromDb.FirstName,
                LastName = clientFromDb.LastName,
                BirthDay = clientFromDb.BirthDay.ToString(FormatConstant.DATE_TIME_FORMAT).Replace('.','/'),
                Email = clientFromDb.Email,
                Id = clientId,
                IsActive = clientFromDb.IsActive
            };
            return model;
        }

        public async Task<ListFromClients> GetAllClients()
        {
            var model = new ListFromClients();
            var clientsFromDB = await database.Clients.ToListAsync();
            foreach (var client in clientsFromDB)
            {
                ShowClientModel clientModel = new ShowClientModel()
                {
                    FullName = client.FirstName + " " + client.LastName,
                    Email = client.Email,
                    Id = client.Id     
                };
                model.Clients.Add(clientModel);
            }
            model.Clients = model.Clients.OrderBy(c => c.FullName).ToHashSet();
            return model;
        }

        public async Task<(bool, string?)> EditClient(ClientInfoModel model)
        {
            (bool isValidModel, string errors) = await ValidateClientModel(model.FirstName, model.LastName, model.Email, model.BirthDay, model.Id);

            if(model.IsActive != true && model.IsActive != false)
            {
                isValidModel = false;
                errors += $"{CommonMessage.ACTIVE_STATUS_ERROR}";
            }
            if (!isValidModel)
            {
                return (isValidModel, errors);
            }
            var originState = await database.Clients.Where(c => c.Id == model.Id).FirstOrDefaultAsync();
            var birthDay = DateTime.ParseExact(model.BirthDay, FormatConstant.DATE_TIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None);
            if (originState.FirstName == model.FirstName && originState.LastName == model.LastName
                && originState.Email == model.Email && originState.IsActive == model.IsActive
                && originState.BirthDay == birthDay)
            {
                return (false, CommonMessage.NO_CHANGES_MESSAGE);
            }
            try
            {
                originState.FirstName = model.FirstName;
                originState.LastName = model.LastName;
                originState.Email = model.Email;
                originState.IsActive = model.IsActive;
                originState.BirthDay = birthDay;
                await database.SaveChangesAsync();
            }
            catch
            {
                return(false, CommonMessage.DATABASE_ERROR);
            }

            return (true, null);
        }

        private async Task<(bool, string)> ValidateClientModel(string firstName, string lastName, string email, string birthDay, string? clientId = null)
        {
            bool isValidModel = true;
            var errors = new StringBuilder();

            if (firstName == null || firstName.Length < NAME_MIN_LENGTH || firstName.Length > FIRST_NAME_MAX_LENGTH)
            {
                isValidModel = false;
                errors.AppendLine($"{nameof(firstName)} must be between {NAME_MIN_LENGTH} and {FIRST_NAME_MAX_LENGTH} characters!");
            }
            if (lastName == null || lastName.Length < NAME_MIN_LENGTH || lastName.Length > LAST_NAME_MAX_LENGTH)
            {
                isValidModel = false;
                errors.AppendLine($"{nameof(lastName)} must be between {NAME_MIN_LENGTH} and {LAST_NAME_MAX_LENGTH} characters!");
            }
            if (email == null || email.Length > EMAIL_MAX_LENGTH)
            {
                isValidModel = false;
                errors.AppendLine($"{nameof(email)} must be maximum from {EMAIL_MAX_LENGTH} symbols");
            }

            var isValidDate = DateTime.TryParseExact(birthDay, FormatConstant.DATE_TIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime birthDayChecked);

            if (birthDayChecked > DateTime.Now)
            {
                isValidDate = false;
                errors.AppendLine("Birth day is in the future!");
            }

            if (!isValidDate)
            {
                isValidModel = false;
                errors.AppendLine($"{nameof(birthDay)} must be valid DATE like '{FormatConstant.DATE_TIME_FORMAT_EXAM}'!");
            }

            bool isEmailChanged = await database.Clients.Where(c => c.Id == clientId && c.Email != email).AnyAsync();
            bool isEmailExist = false;
            
            if (isEmailChanged)
            {
                isEmailExist = await database.Clients.Where(c => c.Email == email).AnyAsync();
            }

            if (isEmailExist)
            {
                isValidModel = false;
                errors.AppendLine("This email is already registred!");
            }

            return (isValidModel, errors.ToString());
        }

    }
}
