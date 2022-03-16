namespace WorkDiaryWebApp.Models.Income
{
    public class PayPostModel
    {

        public string Description { get; set; }
        public decimal Value { get; set; }

        public string clientId { get; set; }
        public string userId { get; set; }
        public string visitBagId { get; set; }
    }
}
