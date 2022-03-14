using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkDiaryWebApp.WorkDiaryDB.Models
{
    public class ClientProcedure
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey(nameof(Client))]
        public string ClientId { get; set; }
        public virtual Client Client { get; set; }

        [ForeignKey(nameof(Procedure))]
        public string ProcedureId { get; set; }
        public virtual Procedure Procedure { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(VisitBagId))]
        public string? VisitBagId { get; set; }
    }
}
