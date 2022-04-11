using WorkDiaryWebApp.WorkDiaryDB.Models;

namespace WorkDiaryWebApp.Models.MainBank
{
    public class MainBankInfoModel
    {
        public decimal Balance { get; set; }
        public List<Report> Reports { get; set; }

    }
}
