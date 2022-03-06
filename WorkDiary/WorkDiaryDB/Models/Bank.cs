using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkDiaryDB.Models
{
    public class Bank
    {
        [Key]
        public string Id { get; init; }

        [Column(TypeName = "decimal(18,2)")]

        //Amount must be in other section???
        public decimal Amount 
        {
            get { return (this.Incomes.Select(i => i.Value).Sum() - this.Outcomes.Select(o => o.Value).Sum()); }
        }
        public virtual ICollection<Income> Incomes { get; set; } = new HashSet<Income>();
        public virtual ICollection<Outcome> Outcomes { get; set; } = new HashSet<Outcome>();
    }
}
