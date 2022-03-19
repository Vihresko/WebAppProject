using WorkDiaryWebApp.Models.Contact;

namespace WorkDiaryWebApp.Core.Interfaces
{
    public interface IContactService
    {
        public List<UserContactGetModel> GetAllContacts();
    }
}
