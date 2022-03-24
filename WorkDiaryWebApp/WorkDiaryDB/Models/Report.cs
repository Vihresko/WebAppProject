using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkDiaryWebApp.WorkDiaryDB.Constraints;

namespace WorkDiaryWebApp.WorkDiaryDB.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }

        [Required]
        [MaxLength(Constants.USERNAME_MAX_LENGTH)]
        public string Username { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
