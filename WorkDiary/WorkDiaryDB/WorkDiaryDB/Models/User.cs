using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkDiaryDB.Models
{
    using static Constraints.Constants;
    public class User
    {
        [Key]
        [Required]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MinLength(USERNAME_MIN_LENGTH)]
        [MaxLength(USERNAME_MAX_LENGTH)]
        public string Username { get; init; }

        [Required]
        [MaxLength(FULL_NAME_MAX_LENGTH)]
        public string FullName { get; private set; }

        [Required]
        [MaxLength(EMAIL_MAX_LENGTH)]
        public string Email { get; private set; }

        [Required]
        public string Password { get; private set; }

        public virtual ICollection<ClientProcedure> UserPlayers { get; set; } = new HashSet<ClientProcedure>();

        public virtual Contact Contact { get; set; }

        [ForeignKey(nameof(Bank))]
        public string BankId { get; private set; }
        public Bank Bank { get; set; }

        //TODO:IsAdmin?
    }
}