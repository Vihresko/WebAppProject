using System.Globalization;
using System.Text;
using WorkDiaryCore.Constraints.Interfaces;
using WorkDiaryCore.Models.Client;
using WorkDiaryDB;
using WorkDiaryDB.Models;
namespace WorkDiaryCore.Constraints.Services
{
    using static WorkDiaryDB.Constraints.Constants;
    public class ClientService : IClientService
    {
        private readonly WorkDiaryDbContext database;
        public ClientService(WorkDiaryDbContext db)
        {
            database = db;
        }
        public (bool isDone, string errors) AddNewClient(object modelObject)
        {
            ClientModel model = (ClientModel)modelObject;
            bool isValidModel = true;
            var errors = new StringBuilder();
            if(model.FirstName.Length > FIRST_NAME_MAX_LENGTH)
            {
                isValidModel = false;
                errors.AppendLine($"{nameof(model.FirstName)} must be maximum from {FIRST_NAME_MAX_LENGTH} characters!");
            }
            if(model.LastName.Length > LAST_NAME_MAX_LENGTH)
            {
                isValidModel = false;
                errors.AppendLine($"{nameof(model.LastName)} must be maximum from {LAST_NAME_MAX_LENGTH} characters!");
            }
            if(model.Email.Length > EMAIL_MAX_LENGTH)
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

    }
}
