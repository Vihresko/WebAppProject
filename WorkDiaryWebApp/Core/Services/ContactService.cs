using WorkDiaryWebApp.Core.Interfaces;
using WorkDiaryWebApp.Models.Contact;
using WorkDiaryWebApp.WorkDiaryDB;

namespace WorkDiaryWebApp.Core.Services
{
    public class ContactService : IContactService
    {
        private readonly WorkDiaryDbContext database;
        public ContactService(WorkDiaryDbContext _database)
        {
            database = _database;
        }
        public List<UserContactGetModel> GetAllContacts()
        {
            var userNames = database.Users.Select(x => new
            {
                FullName = x.FullName,
                ContactId = x.ContactId
            }).ToList();

            var contactsInfo = database.Contacts.ToList();

            var contacts = new List<UserContactGetModel>();

            foreach (var user in userNames)
            {
                var c = contactsInfo.Where(c => c.Id == user.ContactId).FirstOrDefault();
                var contact = new UserContactGetModel()
                {
                    FullName = user.FullName,
                    Address = c.Address,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Town = c.Town
                };
                contacts.Add(contact);
            }
            return contacts;
        }
    }
}
