using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkDiaryDB.Models
{
    using static Constraints.Constants;
    public class Procedure
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();
        [Required]
        [MaxLength(PROCEDURE_NAME_MAX_LENGTH)]
        public string Name { get; private set; }

        [MaxLength(PROCEDURE_DESCRIPTION_MAX_LENGTH)]
        public string? Description { get; private set; }

        [Range((double)PROCEDURE_MIN_PRICE, (double)decimal.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; private set; }
        public virtual ICollection<ClientProcedure> Clients { get; set; } = new HashSet<ClientProcedure>();
    }
}
