using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkDiaryDB.Models
{
    using static Constraints.Constants;
    public class Procedure
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();
        [Required]
        [MinLength(PROCEDURE_NAME_MIN_LENGTH)]
        [MaxLength(PROCEDURE_NAME_MAX_LENGTH)]
        public string Name { get; set; }

        [MaxLength(PROCEDURE_DESCRIPTION_MAX_LENGTH)]
        public string? Description { get; set; }

        [Range((double)PROCEDURE_MIN_PRICE, (double)PROCEDURE_MAX_PRICE)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public virtual ICollection<ClientProcedure> Clients { get; set; } = new HashSet<ClientProcedure>();
    }
}
