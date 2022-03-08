using System.ComponentModel.DataAnnotations;

namespace WorkDiaryWebApp.WorkDiaryDB.Models
{
    public class VisitBag
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public virtual ICollection<ClientProcedure> WorkBag { get; set; } = new List<ClientProcedure>();

    }
}
