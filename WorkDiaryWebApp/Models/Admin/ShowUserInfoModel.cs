namespace WorkDiaryWebApp.Models.Admin
{
    public class ShowUserInfoModel
    {
        public string UserId { get; set; }

        public string Username { get; set; }
        public string UserBankId { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }

        public string Role { get; set; } = null;

    }
}
