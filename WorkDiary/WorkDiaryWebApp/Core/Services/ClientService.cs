using System.Globalization;
using System.Text;
using WorkDiaryDB;
using WorkDiaryDB.Models;
using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.Models.Client;

namespace WorkDiaryWebApp.Constraints.Services
{
    using static WorkDiaryDB.Constraints.Constants;
    public class ClientService : IClientService
    {
        private readonly WorkDiaryDbContext database;
        public ClientService(WorkDiaryDbContext db)
        {
            database = db;
        }
        public (bool isDone, string errors) AddNewClient(AddClientModel model)
        {
            bool isValidModel = true;
            var errors = new StringBuilder();
            if(model.FirstName == null ||model.FirstName.Length < NAME_MIN_LENGTH || model.FirstName.Length > FIRST_NAME_MAX_LENGTH)
            {
                isValidModel = false;
                errors.AppendLine($"{nameof(model.FirstName)} must be between {NAME_MIN_LENGTH} and {FIRST_NAME_MAX_LENGTH} characters!");
            }
            if(model.LastName == null || model.LastName.Length < NAME_MIN_LENGTH || model.LastName.Length > LAST_NAME_MAX_LENGTH)
            {
                isValidModel = false;
                errors.AppendLine($"{nameof(model.LastName)} must be between {NAME_MIN_LENGTH} and {LAST_NAME_MAX_LENGTH} characters!");
            }
            if(model.Email == null ||model.Email.Length > EMAIL_MAX_LENGTH)
            {
                isValidModel = false;
                errors.AppendLine($"{nameof(model.Email)} must be maximum from {EMAIL_MAX_LENGTH} symbols");
            }

            var isValidDate = DateTime.TryParseExact(model.BirthDay, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime birthDay);

            if (!isValidDate)
            {
                isValidModel = false;
                errors.AppendLine($"{nameof(model.BirthDay)} must be valid DATE like '01/01/2000'!");
            }

            if (!isValidModel)
            {
                return (isValidModel, errors.ToString());
            }

            bool isEmailExist = database.Clients.Where(c => c.Email == model.Email).Any();
            if (isEmailExist)
            {
                return (false, "This email is already registred!");
            }

            Client newClient = new Client()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                BirthDay = birthDay
            };

            try
            {
                database.Clients.Add(newClient);
                database.SaveChanges();
            }
            catch
            {
                return (false, "Fail to add client to database");
            }
            return (true, null);
        }

        public ClientInfoModel ClientInfo(string clientId)
        {
            var clientFromDb = database.Clients.Where(c => c.Id == clientId).FirstOrDefault();
            ClientInfoModel model = new ClientInfoModel()
            {
                FirstName = clientFromDb.FirstName,
                LastName = clientFromDb.LastName,
                BirthDay = clientFromDb.BirthDay,
                Email = clientFromDb.Email,
                Id = clientId
            };
            return model;
        }

        public ListFromClients GetAllClients()
        {
            var model = new ListFromClients();
            var clientsFromDB = database.Clients.ToHashSet();
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
    }
}
