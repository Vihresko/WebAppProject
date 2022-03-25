using WorkDiaryWebApp.Models.Contact;

namespace WorkDiaryWebApp.Core.Interfaces
{
    public interface IContactService
    {
        public Task<List<UserContactGetModel>> GetAllContacts();
    }
}
