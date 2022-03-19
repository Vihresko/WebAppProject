namespace WorkDiaryWebApp.Models.Contact
{
    public class UserContactGetModel
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string? Town { get; set; }

        public string? Address { get; set; }
    }
}
