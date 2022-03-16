namespace WorkDiaryWebApp.Models.Income
{
    public class PayPostModel
    {

        public string Description { get; set; }
        public decimal Value { get; set; }

        public string ClientId { get; set; }
        public string UserId { get; set; }
        public string VisitBagId { get; set; }
    }
}
