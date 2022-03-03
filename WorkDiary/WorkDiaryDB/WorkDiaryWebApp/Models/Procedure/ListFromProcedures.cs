namespace WorkDiaryWebApp.Models.Procedure
{
    public class ListFromProcedures
    {
        public ICollection<ShowProcedureModel> Procedures { get; set; } = new HashSet<ShowProcedureModel>();
    }
}
