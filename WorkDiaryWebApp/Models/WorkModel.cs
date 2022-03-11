using WorkDiaryWebApp.Models.Client;
using WorkDiaryWebApp.Models.Procedure;

namespace WorkDiaryWebApp.Models
{
    public class WorkModel
    {
        private readonly ListFromProcedures procedures;
        public WorkModel(ClientInfoModel client, ListFromProcedures _procedures)
        {
            Id = client.Id;
            FirstName = client.FirstName;
            LastName = client.LastName;
            Email = client.Email;
            BirthDay = client.BirthDay;
            IsActive = client.IsActive;
            procedures = _procedures;
        }
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string BirthDay { get; set; }

        public bool IsActive { get; set; }

        public ListFromProcedures Procedures 
        {
            get => this.procedures;
        } 
    }
}
