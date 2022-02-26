using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkDiaryDB.Models
{
    using static Constraints.Constants;
    public class Income
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(IN_OUT_COME_DESCRIPTION_MAX_LENGTH)]
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal  Value { get; private set; }

        [ForeignKey(nameof(Bank))]
        public string BankId { get; set; }
        public Bank Bank { get; set; }

    }
}
