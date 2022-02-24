using System.ComponentModel.DataAnnotations;

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
        [MaxLength(EMAIL_MAX_LENGTH)]
        public string Email { get; private set; }

        [Required]
        public string Password { get; private set; }

        public virtual ICollection<ClientProcedure> UserPlayers { get; set; } = new HashSet<ClientProcedure>();

        public Contact Contact { get; set; }

        //TODO:IsAdmin?
    }
}