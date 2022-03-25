using Microsoft.EntityFrameworkCore;
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
        public async Task<List<UserContactGetModel>> GetAllContacts()
        {
            var userNames = await database.Users.Select(x => new
            {
                FullName = x.FullName,
                ContactId = x.ContactId
            }).ToListAsync();

            var contactsInfo = await database.Contacts.ToListAsync();

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
