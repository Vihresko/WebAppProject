namespace WorkDiaryWebApp.Models.Client
{
    public class AddClientPostModel
    {
       
        public string FirstName { get; private set; }

        public string? LastName { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDay { get; private set; }
    }
}
