namespace WorkDiaryWebApp.Models.Client
{
    public class ListFromClients
    {
        public ICollection<ShowClientModel> Clients { get; set; } = new HashSet<ShowClientModel>();
    }
}
