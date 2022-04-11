using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkDiaryWebApp.WorkDiaryDB.Constraints;

namespace WorkDiaryWebApp.WorkDiaryDB.Models
{
    public class MainBank
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(Constants.MAIN_BANK_NAME_MAX_LENGTH)]
        public string Name { get; set; } = "Vault";

        [Column(TypeName = "decimal(18,2)")]

        public decimal Balance { get; set; }

        public ICollection<Report> Reports { get; set; } = new List<Report>();
        public ICollection<Outcome> Outcomes { get; set; } = new List<Outcome>();
    }
}
