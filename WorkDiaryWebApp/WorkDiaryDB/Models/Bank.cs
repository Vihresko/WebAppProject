using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkDiaryWebApp.WorkDiaryDB.Models
{
    public class Bank
    {
        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Column(TypeName = "decimal(18,2)")]

        //Amount must be in other section???
        public decimal Amount 
        {
            get { return (this.Incomes.Select(i => i.Value).Sum() - this.Outcomes.Select(o => o.Value).Sum()); }
        }
        public virtual ICollection<Income> Incomes { get; set; } = new HashSet<Income>();
        public virtual ICollection<Outcome> Outcomes { get; set; } = new HashSet<Outcome>();

        [Column(TypeName = "decimal(18,2)")]
        public decimal TakenMoney { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ReportedMoney { get; set; }
    }
}
