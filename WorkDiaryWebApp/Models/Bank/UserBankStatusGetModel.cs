namespace WorkDiaryWebApp.Models.Bank
{
    public class UserBankStatusGetModel
    {
        public string UserId { get; set; }
        public decimal TakenMoney { get; set; }
        public decimal ReportedMoney { get; set; }
    }
}
