using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkDiaryWebApp.WorkDiaryDB.Models
{
    using static WorkDiaryDB.Constraints.Constants;
    public class Outcome
    {
        [Key]
        public long Id { get; init; } 

        [Required]
        [MaxLength(IN_OUT_COME_DESCRIPTION_MAX_LENGTH)]
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; private set; }

        [ForeignKey(nameof(Bank))]
        public string BankId { get; set; }
        public virtual Bank Bank { get; set; }
    }
}
